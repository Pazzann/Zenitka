using Godot;
using System;
using System.Numerics;
using Vector2 = Godot.Vector2;
using Vector3 = Godot.Vector3;

public partial class Camera3DFly : Godot.Camera3D
{
	private const float SENSITIVITY = 0.25f;
	private Vector2 _mousePosition = new Vector2(0f, 0f);
	private float _total_pitch = 0.0f;
	private Vector3 _direction = new Vector3(0, 0, 0);
	private Vector3 _velocity = new Vector3(0, 0, 0);
	private float _acceleration = 30f;
	private float _deceleration = -10f;
	private float _velMultiplier = 4;
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
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
		float deltaF = Convert.ToSingle(delta);
		float d = Convert.ToSingle(Input.IsActionPressed("cam_right_3d"));
		float a = Convert.ToSingle(Input.IsActionPressed("cam_left_3d"));
		float s = Convert.ToSingle(Input.IsActionPressed("cam_down_3d"));
		float w = Convert.ToSingle(Input.IsActionPressed("cam_up_3d"));
		if (Input.IsActionPressed("right_click")) Input.MouseMode = Input.MouseModeEnum.Captured;
		else Input.MouseMode = Input.MouseModeEnum.Visible;
		_direction=new Vector3(d-a,0.0f , s-w);
		var offset = _direction.Normalized() * _acceleration * _velMultiplier * deltaF +
					 _velocity.Normalized() * _deceleration * _velMultiplier * deltaF;
		if (_direction == Vector3.Zero && offset.LengthSquared() > _velocity.LengthSquared()) _velocity = Vector3.Zero;
		else
		{
			_velocity.X = Mathf.Clamp(_velocity.X + offset.X, -_velMultiplier, _velMultiplier);
			_velocity.Y = Mathf.Clamp(_velocity.Y + offset.Y, -_velMultiplier, _velMultiplier);
			_velocity.Z = Mathf.Clamp(_velocity.Z + offset.Z, -_velMultiplier, _velMultiplier);
		}
		Translate(_velocity*deltaF);

		if (Input.IsActionPressed("right_click") && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			_mousePosition = GetViewport().GetMousePosition();
			var vieewportSize = GetViewport().GetVisibleRect().Size;
			vieewportSize /= 2;
			_mousePosition -= vieewportSize;
			_mousePosition *= SENSITIVITY;
			var yaw = _mousePosition.X;
			var pitch = _mousePosition.Y;
			_mousePosition = Vector2.Zero;
			pitch = Mathf.Clamp(pitch, -90 - _total_pitch, 90 - _total_pitch);
			_total_pitch += pitch;
			RotateY(Mathf.DegToRad(-yaw));
			RotateObjectLocal(new Vector3(1f, 0f, 0f), Mathf.DegToRad(-pitch));
		}
		else
		{
			_mousePosition = Vector2.Zero;
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			_mousePosition = eventMouseMotion.Position;
			GD.Print(GetViewport().GetMousePosition());
			var vieewportSize = GetViewport().GetVisibleRect().Size;
			vieewportSize /= 2;
			_mousePosition -= vieewportSize;
		}

	}
}
