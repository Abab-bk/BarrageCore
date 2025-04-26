using BarrageCore.Models;

namespace BarrageCore.Runtime;

public class BarrageBullet(
    BulletDef bulletDef,
    Emitter emitter,
    float initialAngle
    ) : IUpdatable
{
    public Action OnDestroy { get; set; } = delegate { };
    public Action<float, float, float> OnMove { get; set; } = delegate { };
    
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Rotation => _rotation.Degrees;

    private Rotation _rotation = new (initialAngle);

    private float _lifeAccumulator;
    
    public void Update(float delta)
    {
        if (_lifeAccumulator >= emitter.EmitterDef.BulletLife) return;
        
        _lifeAccumulator += delta;
        _rotation += emitter.EmitterDef.RotationSpeed;
        X += emitter.EmitterDef.BulletSpeed * MathF.Cos(Rotation * (MathF.PI / 180f)) * delta;
        Y += emitter.EmitterDef.BulletSpeed * MathF.Sin(Rotation * (MathF.PI / 180f)) * delta;
        OnMove.Invoke(X, Y, Rotation);

        if (_lifeAccumulator >= emitter.EmitterDef.BulletLife)
        {
            BarrageManager.Instance?.RemoveEntity(this);
            OnDestroy.Invoke();
        }
    }
}