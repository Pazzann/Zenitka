using Godot;
using System;
using Zenitka.Scripts._2D;

public partial class Camera2DTarget : Camera2D
{
	private Target _target;
	private float _zoom = 1f;
	private int _timeBeforeStart;
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
		if (_timeBeforeStart >= 156)
		{

			try
			{
				_target = GetNode<Target>("/root/Main2D/Target");
			}
			catch
			{
			}



			if (_target != null)
			{
				var targetPosition = _target.Position;
				var currentZoom = Zoom;
				Position = targetPosition;
				if (Input.IsActionJustPressed("cam_zoom_out_3d") && _zoom >= 1.0f)
				{
					_zoom -= 0.5f;
					currentZoom.X = _zoom;
					currentZoom.Y = _zoom;
				}

				if (Input.IsActionJustPressed("cam_zoom_in_3d") && _zoom < 6f)
				{
					_zoom += 0.5f;
					currentZoom.X = _zoom;
					currentZoom.Y = _zoom;
				}

				Zoom = currentZoom;
			}
			else
			{
				_timeBeforeStart = 108;
			}
		}
		else
		{
			_timeBeforeStart++;
		}
	}
}
