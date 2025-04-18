using Godot;
using System.IO;
using BarrageCore.Models;
using BarrageCore.Runtime;
using VYaml.Serialization;

namespace BarrageCoreDemo;

public partial class Main : Node2D
{
    [Export] private OptionButton _patternsOptionsBtn;
    [Export] private Button _spawnBtn;
    [Export] private Button _reloadBtn;

    private BarrageManager _barrageManager;
    
    public override void _Ready()
    {
        _barrageManager = new BarrageManager(new BarrageEventReceiver(this));
        ReloadPatterns();
        _spawnBtn.Pressed += () => _barrageManager.SpawnPattern(
            _patternsOptionsBtn.GetItemText(_patternsOptionsBtn.Selected)
            );
        _reloadBtn.Pressed += ReloadPatterns;
    }

    public void ReloadPatterns()
    {
        GD.Print("Reloading patterns");
        _patternsOptionsBtn.Clear();
        _barrageManager.Reset();
        foreach (var patternFileName in Directory.GetFiles("Patterns"))
        {
            var patternDef = YamlSerializer.Deserialize<PatternDef>(
                File.ReadAllBytes(patternFileName)
                );
            _barrageManager.LoadPattern(patternDef);
            _patternsOptionsBtn.AddItem(patternDef.Id);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _barrageManager.Update((float)delta);
    }
}
