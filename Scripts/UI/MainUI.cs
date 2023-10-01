using Godot;
using System;

public partial class MainUI : Control
{
	// Called when the node enters the scene tree for the first time.
	public void Button2D()
	{
		GetTree().ChangeSceneToFile("res://Scenes/CannonType.tscn");
	}
	private void Button3D()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Main3D.tscn");
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



