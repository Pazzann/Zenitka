using Godot;
using System;

using Zenitka.Scripts.Math;

namespace Zenitka.Scripts._2D
{
	public partial class Target : RigidBody2D {
		private bool _isOffscreen = false;
		private bool _isWithinRange = false;

		protected ParticleState2D _state;
		protected float _simulationTime;

		private Vector2 _currentVelocity;

		public bool UseNumericalIntegration { get; set; } = true;

		public ParticleState2D State {
			get => _state;
			set {
				_state = value;
				
				if (GetParent<Node>() != null)
					Reset();
			}
		}

		public bool IsExploded = false;
		private bool _firstFrameAfterExplosion = true;

		public override void _Ready()
		{
			Reset();
		}
		
		public void ScheduleSelfDestroyWhenOffscreen() {
			_isOffscreen = GetViewportRect().HasPoint(Position);
		}

		public override void _IntegrateForces(PhysicsDirectBodyState2D physicsState)
		{
			
			base._IntegrateForces(physicsState);

			physicsState.LinearVelocity = _currentVelocity;
			Rotation = LinearVelocity.Angle();
		}
		
		public override void _PhysicsProcess(double delta)
		{
			_simulationTime += (float) delta;

			if (UseNumericalIntegration) {
				var pos = GlobalPosition;
				var vel = _currentVelocity;

				State.Integrate(ref pos, ref vel, (float) delta);

				_currentVelocity = vel;
			} else	
				_currentVelocity = State.ComputeVelocity(_simulationTime);

			var rand = new Random();
			
			bool kindX = rand.Next(2) == 1;
			int randCoefX = rand.Next(Settings.Settings2D.Random);
			float randEnvX = (kindX)? 1 - randCoefX * 0.001f : 1 + randCoefX * 0.001f;
			
			bool kindY = rand.Next(2) == 1;
			int randCoefY = rand.Next(Settings.Settings2D.Random);
			float randEnvY = (kindY)? 1 - randCoefY * 0.001f : 1 + randCoefY * 0.001f;
			
			_currentVelocity.X *= randEnvX;
			_currentVelocity.Y *= randEnvY;

			if (IsExploded && _firstFrameAfterExplosion) {
				_currentVelocity /= 5f;
				_firstFrameAfterExplosion = false;
			}
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
			/*if (!_isOffscreen) {
				QueueFree();
			}*/
		}

		protected void Reset() {
			GlobalPosition = State.Position;
			LinearVelocity = State.Velocity;
			GravityScale = 0f;

			_currentVelocity = State.Velocity;
			_simulationTime = 0f;
		}
	}
}
