using Godot;
using System;
using Zenitka.Scripts._3D;

public partial class CameraTarget : Camera3D
{
	private const float ZOOM_SPEED = 0.2f;
	
	private float _zoom = 1f;
	private int _timeBeforeStart;
	private Target _target;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
			// _target = GetNodeOrNull<Target>("/root/Main3D/Target");

			// if (_target != null)
			// {
			// 	if (Input.IsActionPressed("cam_zoom_in_3d") && _zoom - ZOOM_SPEED > 0.3f)
			// 	{
			// 		_zoom -= ZOOM_SPEED;
			// 	}

			// 	if (Input.IsActionPressed("cam_zoom_out_3d") && _zoom + ZOOM_SPEED < 10f)
			// 	{
			// 		_zoom += ZOOM_SPEED;
			// 	}

			// 	var targetPosition = _target.Position;
			// 	var targetBasis = _target.Basis;
			// 	var targetLinearVelocity = _target.LinearVelocity;
			// 	Basis = targetBasis;
			// 	targetPosition.X -= targetLinearVelocity.X * _zoom;
			// 	targetPosition.Z -= targetLinearVelocity.Z * _zoom;
			// 	targetPosition.Y += 10 * _zoom;
			// 	Position = targetPosition;
			// 	Transform = Transform.LookingAt(_target.Position, Vector3.Up);

			// }
	}
}
