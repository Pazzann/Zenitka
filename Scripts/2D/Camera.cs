using Godot;
using System;

public partial class Camera : Camera2D
{
	private Button _button;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_button = GetNodeOrNull<Button>("/root/Main2D/Button2");
		if (Input.IsActionJustPressed("switch1"))
		{
			if (IsCurrent() == false)
			{
				MakeCurrent();
			}
		}
		if (Input.IsActionJustPressed("cam_zoom_out_3d")&&Zoom.X>0.1f)
		{
			Zoom = Zoom / 1.3f;
			Position = new Vector2(Position.X * 1.3f, Position.Y * 1.3f);
		}
		if (Input.IsActionJustPressed("cam_zoom_in_3d"))
		{
			Zoom = Zoom * 1.3f;
			Position= new Vector2(Position.X/1.3f, Position.Y/1.3f);
		}
	}
}
