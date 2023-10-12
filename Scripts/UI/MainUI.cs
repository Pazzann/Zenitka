using Godot;
using System;

public partial class MainUI : Control
{
	// Called when the node enters the scene tree for the first time.
	private void Button2D()
	{
		GetTree().ChangeSceneToFile("res://Scenes/DefaultCannon2DUI.tscn");
	}


	private void ButtonRocket2D()
	{
		GetTree().ChangeSceneToFile("res://Scenes/RocketCannon2DUI.tscn");
	}


	private void Button3D()
	{
		GetTree().ChangeSceneToFile("res://Scenes/DefaultCannon3DUI.tscn");
	}


	private void ButtonRocket3D()
	{
		GetTree().ChangeSceneToFile("res://Scenes/RocketCannon3DUI.tscn");
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}







