using Godot;
using System;

public partial class TargetType : Control
{
	// Called when the node enters the scene tree for the first time.
	private void DefaultTargetButton()
	{
		GetTree().ChangeSceneToFile("res://Scenes/DefaultTarget2DUI.tscn");
	}


	private void RocketTargetButton()
	{
		GetTree().ChangeSceneToFile("res://Scenes/RocketTarget2DUI.tscn");
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



