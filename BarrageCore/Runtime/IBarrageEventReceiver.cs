namespace BarrageCore.Runtime;

public interface IBarrageEventReceiver
{
    public void OnBulletCreated(BarrageBullet bullet);
    public void OnBulletDestroyed(BarrageBullet bullet);
}