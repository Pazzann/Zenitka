using Godot;

namespace Zenitka.Scripts._3D
{
	public partial class DynamicBody : RigidBody3D
	{
		private BodyState3D _state = new BodyState3D();
		private Transform3D _initialTransform;

		private Vector3 _currentVelocity;
		private Vector3 _currentPosition;

		public BodyState3D State
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

			var deltaF = (float) delta;

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
