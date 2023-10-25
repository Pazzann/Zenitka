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
		public Vector2 StartPosition { get; set; }
		public float ConstantAcceleration { get; set; }

		
		public float CurrentTime = 0.0f;
		public float StartAngle = 0.0f;

		public bool IsExploded;

		public override void _Ready()
		{
			StartPosition = new Vector2(Position.X, Position.Y);
		}
		
		public void ScheduleSelfDestroyWhenOffscreen() {
			_isOffscreen = GetViewportRect().HasPoint(Position);
		}

		public override void _IntegrateForces(PhysicsDirectBodyState2D state)
		{
			
			base._IntegrateForces(state);
			
			Rotation = state.LinearVelocity.Normalized().Angle();
			
			var velX = Math2D.XVelocityFromT(this, CurrentTime);
			var velY = Math2D.YVelocityFromT(this, CurrentTime, Settings.Settings2D.Gravity);
			
				
			if (IsExploded)
			{
				base._IntegrateForces(state);
				state.LinearVelocity = new Vector2(velX, velY) / 3.0f;
				return;
			}
			var rand = new Random();
			
			bool kindX = rand.Next(2) == 1;
			int randCoefX = rand.Next(Settings.Settings2D.Random);
			float randEnvX = (kindX)? 1 - randCoefX * 0.001f : 1 + randCoefX * 0.001f;
			
			bool kindY = rand.Next(2) == 1;
			int randCoefY = rand.Next(Settings.Settings2D.Random);
			float randEnvY = (kindY)? 1 - randCoefY * 0.001f : 1 + randCoefY * 0.001f;
			
			velX *= randEnvX;
			velY *= randEnvY;
			
			state.LinearVelocity = new Vector2(velX, velY);
			
		}

		public override void _PhysicsProcess(double delta)
		{
			CurrentTime += (float)delta;
		}

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

		public virtual void Destroy()
		{
			QueueFree();
		}
		
		private void OnVisibleOnScreenNotifier2dScreenExited()
		{
			if (!_isOffscreen) {
				QueueFree();
			}
		}
	}
}
