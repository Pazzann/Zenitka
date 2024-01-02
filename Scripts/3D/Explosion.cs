using Godot;

namespace Zenitka.Scripts._3D;

public partial class Explosion : StaticBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		(GetNode("AnimationPlayer") as AnimationPlayer).Play("explode");
		GetTree().CreateTimer(1.0).Timeout += QueueFree;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
