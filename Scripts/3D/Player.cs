using Godot;

namespace Zenitka.Scripts._3D
{
	using static Utils;

	public partial class Player : CharacterBody3D
	{
		private const float MOVEMENT_SPEED = 10f;
		private const float MOUSE_SENSITIVITY = 0.002f;

		public override void _Ready()
		{
			base._Ready();
		}

		public override void _Process(double delta)
		{
			var deltaF = (float) delta;
			float speed = MOVEMENT_SPEED * deltaF;

			if (Input.MouseMode != Input.MouseModeEnum.Captured)
				return;

			if (Input.IsActionPressed("cam_right_3d"))
				Move(Vector3.Right, speed);

			if (Input.IsActionPressed("cam_left_3d"))
				Move(Vector3.Left, speed);

			if (Input.IsActionPressed("cam_forward_3d"))
				Move(Vector3.Forward, speed);

			if (Input.IsActionPressed("cam_backward_3d"))
				Move(Vector3.Back, speed);

			if (Input.IsActionPressed("cam_up_3d"))
				Move(Vector3.Up, speed);

			if (Input.IsActionPressed("cam_down_3d"))
				Move(Vector3.Down, speed);
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			base._UnhandledInput(@event);

			if (@event is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
				var mouseEvent = @event as InputEventMouseMotion;

				var newPitch = Mathf.Clamp(Rotation.X - mouseEvent.Relative.Y * MOUSE_SENSITIVITY, -Mathf.Pi / 2f + 0.001f, Mathf.Pi / 2f - 0.001f);
				var newYaw = Rotation.Y - mouseEvent.Relative.X * MOUSE_SENSITIVITY;

				Rotation = new Vector3(newPitch, newYaw, 0f);
			}
		}

		private void Move(Vector3 dir, float magnitude) {
			var delta = SafeNormalize(Vector3.Up * dir.Y);
			delta += SafeNormalize(ProjectOnPlane(Transform.Basis.X * dir.X, Vector3.Up));
			delta += SafeNormalize(ProjectOnPlane(Transform.Basis.Z * dir.Z, Vector3.Up));

			MoveAndCollide(delta * magnitude);
		}
	}
}
