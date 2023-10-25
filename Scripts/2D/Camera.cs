using Godot;
using System;
using System.ComponentModel.Design;

public partial class Camera : Camera2D
{
	private Button _button;
	private Vector2 _mousePosition;
	private Vector2 _startPosition;
	private bool _first = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_first) {_first = false;_startPosition = Position;}
		_mousePosition=GetViewport().GetMousePosition(); ;
		_button = GetNodeOrNull<Button>("/root/Main2D/Button2");
		if (Input.IsActionJustPressed("switch1"))
		{
			if (IsCurrent() == false)
			{
				MakeCurrent();
			}
		}
		if (Input.IsActionJustPressed("cam_zoom_out_3d"))
		{
			Position = new Vector2(0f,-5400f);
			Zoom = new Vector2(0.1f,0.1f);
		}
		if (Input.IsActionJustPressed("cam_zoom_in_3d"))
		{

			_mousePosition = 10 * _mousePosition;
			_mousePosition.Y = Position.Y + _startPosition.Y*(0.1f/Zoom.Y) + _mousePosition.Y * (0.1f / Zoom.Y);
			_mousePosition.X = Position.X - 19200 * 0.5f*(0.1f/Zoom.X) + _mousePosition.X * (0.1f / Zoom.X);
			if(_mousePosition.Y > Position.Y/1.3) { _mousePosition.Y = Position.Y/1.3f; }
			Position = _mousePosition;
			Zoom = Zoom * 1.3f;
		}

	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			_mousePosition = eventMouseMotion.Position;
		}

	}
}
