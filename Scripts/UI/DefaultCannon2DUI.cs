using Godot;
using System;

public partial class DefaultCannon2DUI : Control
{
	// Called when the node enters the scene tree for the first time.
	private void NextButton()
	{
		GetTree().ChangeSceneToFile("res://Scenes/TargetType.tscn");
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



