using Godot;
using BarrageCore.Runtime;

namespace BarrageCoreDemo;

public partial class BulletEntity : Node2D
{
    public BarrageBullet Bullet { get; set; }

    public override void _Ready()
    {
        Bullet.OnMove += (_, _, _) => UpdatePosition();
        Bullet.OnDestroy += QueueFree;
    }

    public override void _PhysicsProcess(double delta)
    {
    }

    private void UpdatePosition()
    {
        GlobalPosition = GlobalPosition with
        {
            X = Bullet.X,
            Y = Bullet.Y
        };
        RotationDegrees = Bullet.Rotation;
    }
}
