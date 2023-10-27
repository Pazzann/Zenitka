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
			physicsState.Transform = physicsState.Transform.LookingAt(physicsState.LinearVelocity, Vector3.Up);
		}

		public override void _PhysicsProcess(double delta)
		{
			base._PhysicsProcess(delta);
			_time += (float) delta;

			GD.Print("-z: ", Transform.Basis.Z);

			//GD.Print((-Transform.LookingAt(-LinearVelocity, Vector3.Up).Basis.Z).AngleTo(-LinearVelocity));

			//GlobalRotation = new Vector3(0f, 0.2f, 0f);

			//GD.Print(LinearVelocity.Cross(Transform.Basis.Z).Length());

			//Transform = Transform.LookingAt(Vector3.Up, Vector3.Forward);
			//GD.Print(LinearVelocity);
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
