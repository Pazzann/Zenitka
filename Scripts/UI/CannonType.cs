using Godot;
using System;

public partial class CannonType : Control
{
	// Called when the node enters the scene tree for the first time.
	
	private void DefaultCannonButton()
	{
		GetTree().ChangeSceneToFile("res://Scenes/DefaultCannon2DUI.tscn");
	}


	private void RocketCannonButton()
	{
		GetTree().ChangeSceneToFile("res://Scenes/RocketCannon2DUI.tscn");
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



