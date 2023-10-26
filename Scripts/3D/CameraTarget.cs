using Godot;
using System;
using Zenitka.Scripts._3D;

public partial class CameraTarget : Camera3D
{
	private const float ZOOM_SPEED = 0.2f;

	private float _zoom = 1f;
	private Target _target;
	private Node3D _rocket;
	private Vector3 _targetLinearVelocityNormalized;
	private Basis _targetBasis;
	private bool _first = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_target = GetNodeOrNull<Target>("/root/Main3D/Target");
		_rocket = GetNodeOrNull<Node3D>("/root/Main3D/Target/Rocket");


		if (_target != null)
		{

			if (Input.IsActionPressed("cam_zoom_in_3d") && _zoom - ZOOM_SPEED > 0.3f)
			{
				_zoom -= ZOOM_SPEED;
			}

			if (Input.IsActionPressed("cam_zoom_out_3d") && _zoom + ZOOM_SPEED < 10f)
			{
				_zoom += ZOOM_SPEED;
			}
			_targetLinearVelocityNormalized = _target.LinearVelocity.Normalized();
			Basis = _target.Basis;
			Position = _target.Position;
			if (_targetLinearVelocityNormalized != new Vector3(0, 0, 0))
			{
				Translate(new Vector3(-_targetLinearVelocityNormalized.X * 5 * _zoom, 10 * _zoom, -_targetLinearVelocityNormalized.Z * 5 * _zoom));
			}
			else
			{ Translate(new Vector3(-5 * _zoom, 10 * _zoom, -5 * _zoom)); }
			Transform = Transform.LookingAt(_target.Position, Vector3.Up);
			if (_target.LinearVelocity != new Vector3(0, 0, 0))
			{
					_rocket.Transform=_rocket.Transform.LookingAt(-_target.LinearVelocity*3,Vector3.Up);
			}
		}

	}
	private void _on_target_spawn_timer_timeout()
	{
		_target = GetNode<Target>("/root/Main3D/Target");
		_rocket = GetNode<Node3D>("/root/Main3D/Target/Rocket");

		if (_target != null)
		{
		}
	}
}



