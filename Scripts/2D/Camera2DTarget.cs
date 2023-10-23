using Godot;
using System;
using Zenitka.Scripts._2D;

public partial class Camera2DTarget : Camera2D
{
	private Target _target;
	private float _zoom = 1f;
	private int _timeBeforeStart=-2;

	private Vector2 _targetStartingPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("switch2"))
		{
			if (IsCurrent() == false)
			{
				MakeCurrent();
			}
		}
		_target = GetNodeOrNull<Target>("/root/Main2D/Target");
		if (_target != null)
		{
			var currentZoom = Zoom;
			Position = _target.Position;
			if (Input.IsActionJustPressed("cam_zoom_out_3d") && _zoom >= 1.0f)
			{
				_zoom -= 0.5f;
				currentZoom.X = _zoom;
				currentZoom.Y = _zoom;
			}

			if (Input.IsActionJustPressed("cam_zoom_in_3d") && _zoom < 20f)
			{
				_zoom += 0.5f;
				currentZoom.X = _zoom;
				currentZoom.Y = _zoom;
			}

			Zoom = currentZoom;
		}
			
	}
	private void _on_target_spawn_timer_timeout()
	{
		_target = GetNode<Target>("/root/Main2D/Target");
		_targetStartingPosition = _target.Position;
	}
}
