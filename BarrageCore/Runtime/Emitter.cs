using BarrageCore.Models;

namespace BarrageCore.Runtime;

public class Emitter(
    EmitterDef emitterDef,
    BulletDef bulletDef
    ) : IUpdatable
{
    public EmitterDef EmitterDef => emitterDef;

    private float _repeatDelayAccumulator;
    private float _shootDelayAccumulator;

    private int _shootCount;
    private int _emittedCount;
    
    public void Update(float delta)
    {
        if (_emittedCount >= emitterDef.Repeat) return;

        if (_shootCount >= emitterDef.ShootCount)
        {
            _repeatDelayAccumulator += delta;
            if (_repeatDelayAccumulator >= emitterDef.RepeatDelay)
            {
                _emittedCount++;
                _shootCount = 0;
                _repeatDelayAccumulator = 0f;
                _shootDelayAccumulator = 0f;
            }
            else return;
        }
        
        _shootDelayAccumulator += delta;
        var requiredDelay = _shootCount == 0 ? 0f : emitterDef.ShootDelay;

        if (_shootCount < emitterDef.ShootCount &&
            _shootDelayAccumulator >= requiredDelay
            )
        {
            Shoot(EmitterDef.InitialAngle + 
                  emitterDef.AddedAngle * _shootCount
                  );
            _shootCount++;
            _shootDelayAccumulator -= requiredDelay;
        }

        if (_emittedCount >= emitterDef.Repeat) BarrageManager.Instance?.RemoveEntity(this);
    }

    private void Shoot(float initialAngle)
    {
        var bullet = BarrageManager.Instance?.NewBullet(
            bulletDef,
            this,
            initialAngle
            );
        if (bullet == null) return;
        BarrageManager.Instance?.AddBulletToWorld(bullet);
    }
}