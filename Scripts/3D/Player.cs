using Godot;

namespace Zenitka.Scripts._3D
{
	public partial class Player : CharacterBody3D
	{
		private const float MOVEMENT_SPEED = 10f;
		private const float MOUSE_SENSITIVITY = 0.002f;

		public override void _Ready()
		{
			base._Ready();

			Transform = Transform.LookingAt(new Vector3(0f, 0f, 0.5f), Vector3.Up);
		}

		public override void _Process(double delta)
		{
			var deltaF = (float) delta;

			if (Input.MouseMode != Input.MouseModeEnum.Captured)
				return;

			if (Input.IsActionPressed("cam_right_3d"))
				MoveAndCollide(Transform.Basis.X * MOVEMENT_SPEED * deltaF);

			if (Input.IsActionPressed("cam_left_3d"))
				MoveAndCollide(-Transform.Basis.X * MOVEMENT_SPEED * deltaF);

			if (Input.IsActionPressed("cam_forward_3d"))
				MoveAndCollide(-Transform.Basis.Z * MOVEMENT_SPEED * deltaF);

			if (Input.IsActionPressed("cam_backward_3d"))
				MoveAndCollide(Transform.Basis.Z * MOVEMENT_SPEED * deltaF);

			if (Input.IsActionPressed("cam_up_3d"))
				MoveAndCollide(Vector3.Up * MOVEMENT_SPEED * deltaF);

			if (Input.IsActionPressed("cam_down_3d"))
				MoveAndCollide(Vector3.Down * MOVEMENT_SPEED * deltaF);
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
	}
}
