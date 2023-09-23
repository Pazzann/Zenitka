using System;
using Godot;


namespace Zenitka.Scripts._2D
{
	public partial class Cannon : Node
	{

		public float GunRotationSpeed = 5f;
		
		private Sprite2D _gun;
		private Node2D _head;
		
		private float _targetAngle = 0;
		private bool _shouldSignal = false;

		public override void _Ready()
		{
			_gun = GetNode<Sprite2D>("Body/Gun");
			_head = GetNode<Node2D>("Body/Gun/Head");
		}

		public Vector2 GetHeadPosition() {
			return _head.GlobalPosition;
		}

		public void RotateToAndSignal(float targetAngle) {
			_shouldSignal = true;
			_targetAngle = 0.5f * Mathf.Pi - Mathf.PosMod(targetAngle, 2f * Mathf.Pi);
		}
		
		public override void _PhysicsProcess(double delta)
		{
			float deltaF = (float) delta;

			if (Mathf.IsEqualApprox(_gun.Rotation, _targetAngle, deltaF * GunRotationSpeed)) {
				if (_shouldSignal) {
					_shouldSignal = false;
					EmitSignal(SignalName.GunReady, 0.5f * Mathf.Pi - _targetAngle, GetHeadPosition());
				}

				return;
			} else
				_gun.Rotation += Mathf.Sign(_targetAngle - _gun.Rotation) * GunRotationSpeed * deltaF;
		}

		[Signal]
		public delegate void GunReadyEventHandler(float angleRad, Vector2 headPosition);
	}

}

