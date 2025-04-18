using BarrageCore.Runtime;
using Godot;

namespace BarrageCoreDemo;

public class BarrageEventReceiver(Node2D world) : IBarrageEventReceiver
{
    public void OnBulletCreated(BarrageBullet bullet)
    {
        var bulletEntity = GD
            .Load<PackedScene>("res://BulletEntity.tscn")
            .Instantiate<BulletEntity>();
        bulletEntity.Bullet = bullet;
        world.AddChild(bulletEntity);
    }

    public void OnBulletDestroyed(BarrageBullet bullet)
    {
        throw new System.NotImplementedException();
    }
}