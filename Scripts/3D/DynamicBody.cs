using Godot;

namespace Zenitka.Scripts._3D
{
	public partial class DynamicBody : RigidBody3D
	{
		private ParticleState3D _state = new ParticleState3D();
		private Transform3D _initialTransform;

		public ParticleState3D State
		{
			get { return _state; }
			set
			{
				_state = value;
				Reset();
			}
		}

		private float _time = 0f;

		public override void _Ready()
		{
			Reset();
			_initialTransform = Transform;
		}

		public override void _IntegrateForces(PhysicsDirectBodyState3D physicsState)
		{
			base._IntegrateForces(physicsState);

			physicsState.LinearVelocity = State.ComputeVelocity(_time);
			physicsState.Transform = physicsState.Transform.LookingAt(GlobalPosition + physicsState.LinearVelocity, Vector3.Up);
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			_time += (float) delta;
		}

		private void Reset()
		{
			GlobalPosition = State.Position;
			LinearVelocity = Vector3.Zero;
			ConstantForce = Vector3.Zero;
			GravityScale = 0f;
		}
	}
}
