using BarrageCore.Models;

namespace BarrageCore.Runtime;

public class BarrageManager
{
    public static BarrageManager? Instance;
    
    private readonly Dictionary<string, PatternDef> _patternDefs = [];
    
    private readonly List<IUpdatable> _activeEntities = [];
    private readonly List<IUpdatable> _pendingAdds = [];
    private readonly List<IUpdatable> _pendingRemovals = [];
    
    private readonly IBarrageEventReceiver _eventReceiver;
    
    public BarrageManager(IBarrageEventReceiver eventReceiver)
    {
        Instance = this;
        _eventReceiver = eventReceiver;
    }
    
    internal void RemoveEntity(IUpdatable entity) => _pendingRemovals.Add(entity);

    public void Reset()
    {
        _pendingAdds.Clear();
        _pendingRemovals.Clear();
        _activeEntities.Clear();
        _patternDefs.Clear();
    }

    public void LoadPattern(PatternDef patternDef) =>
        _patternDefs.Add(patternDef.Id, patternDef);

    public void SpawnPattern(string patternId)
    {
        if (!_patternDefs.TryGetValue(patternId, out var patternDef)) return;

        foreach (var emitterDef in patternDef.Emitters)
        {
            var bulletDef = patternDef.Bullets
                .FirstOrDefault(b => b.Id == emitterDef.Bullet);
            if (bulletDef == null) continue;
            
            var emitter = new Emitter(emitterDef, bulletDef);
            // _activeEntities.Add(emitter);
            _pendingAdds.Add(emitter);
        }
    }

    public BarrageBullet NewBullet(BulletDef bulletDef, Emitter emitter) =>
        new(bulletDef, emitter);
    
    public void AddBulletToWorld(BarrageBullet bullet)
    {
        // _activeEntities.Add(bullet);
        _pendingAdds.Add(bullet);
        _eventReceiver.OnBulletCreated(bullet);
    }

    public void Update(float delta)
    {
        if (_pendingRemovals.Count > 0)
        {
            foreach (var entity in _pendingRemovals)
                _activeEntities.Remove(entity);
            _pendingRemovals.Clear();
        }
        
        if (_pendingAdds.Count > 0)
        {
            _activeEntities.AddRange(_pendingAdds);
            _pendingAdds.Clear();
        }
        
        for (var i = _activeEntities.Count - 1; i >= 0; i--)
        {
            var entity = _activeEntities[i];
            entity.Update(delta);
        }
    }
}