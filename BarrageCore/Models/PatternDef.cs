using VYaml.Annotations;

namespace BarrageCore.Models;

[YamlObject]
public partial record class PatternDef(
    string Id,
    List<BulletDef> Bullets,
    List<EmitterDef> Emitters
    );