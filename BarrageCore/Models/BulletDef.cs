using VYaml.Annotations;

namespace BarrageCore.Models;

[YamlObject]
public partial record BulletDef(
    string Id,
    float Size
    );