using Godot;

namespace Zenitka.Scripts._3D
{

	public struct BodyState
	{
		public Vector3 Position, Velocity, ConstantAcceleration;
		public float LinearDrag, SelfPropellingAcceleration, Mass;

		public BodyState()
		{
			Position = Vector3.Zero;
			Velocity = Vector3.Zero;
			ConstantAcceleration = Vector3.Zero;
			LinearDrag = 0f;
			SelfPropellingAcceleration = 0f;
			Mass = 1f;
		}

		public BodyState(Vector3 position,
						   Vector3 velocity,
						   Vector3 constantAcceleration,
						   float linearDrag,
						   float selfPropellingAcceleration,
						   float mass)
		{
			Position = position;
			Velocity = velocity;
			ConstantAcceleration = constantAcceleration;
			LinearDrag = linearDrag;
			SelfPropellingAcceleration = selfPropellingAcceleration;
			Mass = mass;
		}

		public readonly Vector3 ComputeAcceleration(in Vector3 curVelocity)
		{
			var a = ConstantAcceleration - LinearDrag / Mass * curVelocity;

			if (curVelocity != Vector3.Zero)
				a += curVelocity.Normalized() * SelfPropellingAcceleration;

			return a;
		}

		public readonly Vector3 ComputeAccelerationDerivative(in Vector3 curVelocity, in Vector3 acceleration)
		{
			var a = -LinearDrag / Mass * Vector3.One;

			if (curVelocity != Vector3.Zero)
				a += curVelocity.Normalized() * SelfPropellingAcceleration;

			return a;
		}

		public readonly void Integrate(ref Vector3 curPosition, ref Vector3 curVelocity, float dt)
		{
			var acceleration = ComputeAcceleration(curVelocity);

			curVelocity += acceleration * dt;
			curPosition += curVelocity * dt;
		}
	}

	public partial class DynamicBody : RigidBody3D
	{
		private BodyState _state = new BodyState();
		private Transform3D _initialTransform;

		private Vector3 _currentVelocity;
		private Vector3 _currentPosition;

		public BodyState State
		{
			get { return _state; }
			set
			{
				_state = value;
				Reset();
			}
		}

		public override void _Ready()
		{
			Reset();
			_initialTransform = Transform;
		}

		public override void _IntegrateForces(PhysicsDirectBodyState3D physicsState)
		{

			base._IntegrateForces(physicsState);

			physicsState.LinearVelocity = _currentVelocity;

			if (_currentVelocity != Vector3.Zero)
				Transform = Transform.LookingAt(_currentPosition + _currentVelocity, Vector3.Up);
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);

			var deltaF = (float)delta;

			var pos = GlobalPosition;
			var vel = _currentVelocity;

			State.Integrate(ref pos, ref vel, deltaF);

			_currentPosition = pos;
			_currentVelocity = vel;
		}

		private void Reset()
		{
			GlobalPosition = _currentPosition = State.Position;
			LinearVelocity = _currentVelocity = State.Velocity;
			GravityScale = 0f;
			ConstantForce = Vector3.Zero;
		}
	}
}
