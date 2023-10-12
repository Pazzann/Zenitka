using Godot;
using System;

public partial class Camera3D : Godot.Camera3D
{
	private const float ROTATION_SPEED = 1f;
	private const float ZOOM_SPEED = 10f;

	public override void _Ready()
	{
		Transform = Transform.LookingAt(new Vector3(0f, 0f, 0.5f), Vector3.Up);
	}

	public override void _Process(double delta)
	{
		var deltaF = (float) delta;
		var angle = deltaF * ROTATION_SPEED;

		if (Input.IsActionPressed("cam_right_3d"))
			RotateAbsolute(Vector3.Up, angle);
		
		if (Input.IsActionPressed("cam_left_3d"))
			RotateAbsolute(Vector3.Up, -angle);
		
		if (Input.IsActionPressed("cam_up_3d"))
			RotateRelative(Vector3.Right, deltaF);
		
		if (Input.IsActionPressed("cam_down_3d"))
			RotateRelative(Vector3.Right, -deltaF);
		
		if (Input.IsActionPressed("cam_zoom_in_3d") && Position.LengthSquared() > 2f)
			Transform = Transform.Translated(-Position.Normalized() * deltaF * ZOOM_SPEED);

		if (Input.IsActionPressed("cam_zoom_out_3d") && Position.LengthSquared() < 1000f)
			Transform = Transform.Translated(Position.Normalized() * deltaF * ZOOM_SPEED);
		if (Input.IsActionJustPressed("switch"))
		{
			if (Current == true)
			{
				Current = false;
			}
			else
			{
				Current = true;
			}
		}
	}

	private void RotateRelative(in Vector3 axis, float angle) {
		Transform = Transform.RotatedLocal(axis, angle);
	}

	private void RotateAbsolute(in Vector3 axis, float angle) {
		Transform = Transform.Rotated(axis, angle);
	}
}
