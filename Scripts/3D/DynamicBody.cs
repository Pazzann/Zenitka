using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class DynamicBody : RigidBody3D {
		private ParticleState3D _state = new ParticleState3D();

		public ParticleState3D State 
		{
			get { return _state; }
			set { 
				_state = value;
				Reset();
			}
		}

		private float _time = 0f;

		public override void _Ready() {
			Reset();
		}

		public override void _IntegrateForces(PhysicsDirectBodyState3D physicsState)
		{
			base._IntegrateForces(physicsState);
			physicsState.LinearVelocity = State.ComputeVelocity(_time);
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			_time += (float) delta;
		}

		private void Reset() {
			GlobalPosition = State.Position;
			LinearVelocity = Vector3.Zero;
			ConstantForce = Vector3.Zero;
			GravityScale = 0f;
		}
	}
}
