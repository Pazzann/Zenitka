using Godot;
using System;

using Zenitka.Scripts.Math;

namespace Zenitka.Scripts._2D
{
	public partial class BallisticBody : RigidBody2D
	{
		private static Random _rng = new Random();

		private bool _isOffscreen = false;
		private bool _isWithinRange = false;

		protected BodyState2D _state;
		protected float _simulationTime;

		private Vector2 _currentVelocity;
		private Vector2 _currentPosition;

		public bool UseNumericalIntegration { get; set; } = true;

		public BodyState2D State
		{
			get => _state;
			set
			{
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

		public void ScheduleSelfDestroyWhenOffscreen()
		{
			// TODO: use the view rect set by the parent
			_isOffscreen = GetViewportRect().HasPoint(GlobalPosition);
		}

		public override void _IntegrateForces(PhysicsDirectBodyState2D physicsState)
		{

			base._IntegrateForces(physicsState);

			physicsState.LinearVelocity = _currentVelocity;
			Rotation = _currentVelocity.Angle();
		}

		public override void _PhysicsProcess(double delta)
		{
			var deltaF = (float) delta;
			_simulationTime += deltaF;

			if (UseNumericalIntegration)
			{
				var pos = GlobalPosition;
				var vel = _currentVelocity;

				State.Integrate(ref pos, ref vel, deltaF);

				_currentPosition = pos;
				_currentVelocity = vel;
			}
			else
				_currentVelocity = State.ComputeVelocity(_simulationTime);

			ApplyRandomness(ref _currentVelocity);

			if (IsExploded && _firstFrameAfterExplosion)
			{
				_currentVelocity /= 5f;
				_firstFrameAfterExplosion = false;
			}
		}

		[Signal]
		public delegate void WentWithinRangeEventHandler();

		private void OnVisibleOnScreenNotifier2dScreenEntered()
		{
			_isOffscreen = false;

			if (!_isWithinRange)
			{
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

		protected void Reset()
		{
			GlobalPosition = State.Position;
			LinearVelocity = State.Velocity;
			GravityScale = 0f;

			_currentPosition = State.Position;
			_currentVelocity = State.Velocity;
			_simulationTime = 0f;
		}

		private void ApplyRandomness(ref Vector2 vel) {
			bool kindX = _rng.Next(2) == 1;
			int randCoefX = _rng.Next(Settings.Settings2D.Random);
			float randEnvX = kindX ? 1 - randCoefX * 0.001f : 1 + randCoefX * 0.001f;

			bool kindY = _rng.Next(2) == 1;
			int randCoefY = _rng.Next(Settings.Settings2D.Random);
			float randEnvY = kindY ? 1 - randCoefY * 0.001f : 1 + randCoefY * 0.001f;

			vel.X *= randEnvX;
			vel.Y *= randEnvY;
		}
	}
}
