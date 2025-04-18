using VYaml.Annotations;

namespace BarrageCore.Models;

[YamlObject]
public partial record EmitterDef(
    int Repeat,
    string Bullet,
    float BulletSpeed,
    int BulletCount,
    float ShootDelay,
    float RotationSpeed,
    float BulletLife
    );