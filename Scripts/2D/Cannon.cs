using System;
using Godot;


namespace Zenitka.Scripts._2D
{
	public partial class Cannon : Node
	{

		public float GunRotationSpeed = 1f;
		
		private Sprite2D _gun;
		
		private float _targetAngle = 0;
		private bool _shouldSignal = false;

		public override void _Ready()
		{
			_gun = GetNode<Sprite2D>("Body/Gun");
		}

		public void RotateToAndSignal(float targetAngle) {
			_shouldSignal = true;
			_targetAngle = targetAngle;
		}
		
		public override void _PhysicsProcess(double delta)
		{
			float deltaF = (float) delta;

			if (Mathf.IsEqualApprox(_gun.Rotation, _targetAngle, deltaF * GunRotationSpeed)) {
				if (_shouldSignal) {
					_shouldSignal = false;
					EmitSignal(SignalName.GunReady, _targetAngle);
				}

				return;
			} else
				_gun.Rotation += Mathf.Sign(_targetAngle - _gun.Rotation) * GunRotationSpeed * deltaF;
		}

		[Signal]
		public delegate void GunReadyEventHandler(float angleRad);
	}

}

