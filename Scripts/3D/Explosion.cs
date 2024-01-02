using Godot;

public partial class Explosion : Node3D
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
