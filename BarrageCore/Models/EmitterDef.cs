using VYaml.Annotations;

namespace BarrageCore.Models;

[YamlObject]
public partial record EmitterDef(
    int Repeat,
    float RepeatDelay,
    string Bullet,
    float BulletSpeed,
    float AddedAngle,
    float InitialAngle,
    int ShootCount,
    float ShootDelay,
    float RotationSpeed,
    float BulletLife
    );