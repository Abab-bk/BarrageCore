using BarrageCore.Models;

namespace BarrageCore.Runtime;

public class Emitter(
    EmitterDef emitterDef,
    BulletDef bulletDef
    ) : IUpdatable
{
    public EmitterDef EmitterDef => emitterDef;

    private float _delayAccumulator;
    private int _emittedCount;
    
    public void Update(float delta)
    {
        if (_emittedCount >= emitterDef.Repeat) return;
        
        _delayAccumulator += delta;
        
        if (_delayAccumulator >= emitterDef.ShootDelay)
        {
            SpawnBulletWave();
            _delayAccumulator = 0;
            _emittedCount++;
        }
        
        if (_emittedCount >= emitterDef.Repeat) BarrageManager.Instance?.RemoveEntity(this);
    }

    private void SpawnBulletWave()
    {
        for (var i = 0; i < emitterDef.BulletCount; i++)
        {
            var bullet = BarrageManager.Instance?.NewBullet(bulletDef, this);
            if (bullet == null) continue;
            BarrageManager.Instance?.AddBulletToWorld(bullet);
        }
    }
}