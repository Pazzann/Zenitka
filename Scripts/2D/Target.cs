using Godot;
using System;
using Zenitka.Scripts.Math;

namespace Zenitka.Scripts._2D
{
	public partial class Target : RigidBody2D {
		private bool _isOffscreen = false;
		private bool _isWithinRange = false;

		public float Weight { get; set; }
		public float DragCoefficient { get; set; }
		public float StartVelocity { get; set; }

		public float CurrentTime = 0.0f;
		public float StartAngle = 0.0f;

		public override void _Ready()
		{
			Weight = 0.0f;
			DragCoefficient = 0.0f;
			StartVelocity = 0.0f;
		}
		
		public void ScheduleSelfDestroyWhenOffscreen() {
			_isOffscreen = GetViewportRect().HasPoint(Position);
		}

		public override void _IntegrateForces(PhysicsDirectBodyState2D state)
		{
			base._IntegrateForces(state);

			float velX = Math2D.XVelocityFromT(this, CurrentTime);
			float velY = Math2D.YVelocityFromT(this, CurrentTime, 9.8f);

			Rotation = (float)(System.Math.Atan(velY / velX) - System.Math.PI/2.0f);

			state.LinearVelocity = new Vector2(velX, velY);
		}

		// public override void _PhysicsProcess(double delta)
		// {
		// 	CurrentTime += (float)delta;
		// 	float velX = Math2D.XVelocityFromT(this, CurrentTime);
		// 	float velY = Math2D.YVelocityFromT(this, CurrentTime, 9.8f);

		// 	LinearVelocity = new Vector2(velX, velY);
		// }

		[Signal]
		public delegate void WentWithinRangeEventHandler();
		
		private void OnVisibleOnScreenNotifier2dScreenEntered()
		{
			_isOffscreen = false;
		
			if (!_isWithinRange) {
				_isWithinRange = true;
		
				ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout)
					.OnCompleted(() => EmitSignal(SignalName.WentWithinRange));
			}
		}
		
		private void OnVisibleOnScreenNotifier2dScreenExited()
		{
			if (!_isOffscreen) {
				GD.Print("target self-destroyed as it went offscreen");
				QueueFree();
			}
		}
	}
}
