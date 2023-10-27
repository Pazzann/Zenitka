using Godot;
using System;
using Zenitka.Scripts._2D;

public partial class Camera2DTarget : Camera2D
{
	private Target _target;
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
		if (_target == null)
		{
			_target = GetNodeOrNull<Target>("/root/Main2D/Rocket1");
		}
		if (_target != null)
		{
			Position = _target.Position;
			Position = _target.Position;
			if (Input.IsActionJustPressed("cam_zoom_out_3d"))
			{
				Zoom = Zoom / 1.3f;
			}
			if (Input.IsActionJustPressed("cam_zoom_in_3d"))
			{
				Zoom = Zoom * 1.3f;
			}
		}

	}
	private void _on_target_spawn_timer_timeout()
	{
		if (_target == null)
		{
			_target = GetNodeOrNull<Target>("/root/Main2D/Rocket1");
		}
		_target = GetNode<Target>("/root/Main2D/Target");
		_targetStartingPosition = _target.Position;
	}
}
