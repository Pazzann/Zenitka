using Godot;
using System;
using Zenitka.Scripts._3D;

public partial class CameraTarget : Camera3D
{
	private const float ZOOM_SPEED = 0.2f;
	private float _koef = 1f;
	private Target _target;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
			_target = GetNode<Target>("/root/Main3D/Target");

		if (_target != null)
		{
			if (Input.IsActionPressed("cam_zoom_in_3d") && _koef - ZOOM_SPEED>0.3f)
			{
				_koef -= ZOOM_SPEED;
			}
			if (Input.IsActionPressed("cam_zoom_out_3d") && _koef - ZOOM_SPEED<10f)
			{
				_koef += ZOOM_SPEED;
			}
			var temp = _target.Position;
			var temp2 = _target.Basis;
			var temp3 = _target.LinearVelocity;
			Basis = temp2;
			temp.X -= temp3.X*_koef;
			temp.Z -= temp3.Z*_koef;
			temp.Y += 10*_koef;
			Position = temp;
			Transform = Transform.LookingAt(_target.Position, Vector3.Up);

		}
	}
	private void RotateRelative(in Vector3 axis, float angle) {
		Transform = Transform.RotatedLocal(axis, angle);
	}

	private void RotateAbsolute(in Vector3 axis, float angle) {
		Transform = Transform.Rotated(axis, angle);
	}
}
