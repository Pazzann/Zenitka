using System;
using Godot;


namespace Zenitka.Scripts._2D
{
	public partial class Cannon : Node2D
	{

		private float _rotationSpeed = 10f;
		
		private Sprite2D _gun;
		private Node2D _head;

		public void SetRotationSpeed(float speed) {
			_rotationSpeed = speed;
		}

		public override void _Ready()
		{
			_gun = GetNode<Sprite2D>("Body/Gun");
			_head = GetNode<Node2D>("Body/Gun/Head");
		}

		public Vector2 GetHeadPosition() {
			return _head.GlobalPosition;
		}

		public float GetAngle() {
			return 0.5f * Mathf.Pi - _gun.Rotation;
		}

		public void RotateToAndSignal(float targetAngle) {
			targetAngle = 0.5f * Mathf.Pi - Mathf.PosMod(targetAngle, 2f * Mathf.Pi);
			var curRot = Mathf.PosMod(Rotation, 2f * Mathf.Pi);
			var diff = Mathf.Abs(curRot - targetAngle);

			if (Mathf.IsEqualApprox(Rotation, targetAngle, 0.001f))
				Signal();
			else {
				var tween = CreateTween();

				tween.TweenProperty(_gun, "rotation", targetAngle, diff / _rotationSpeed)
					.SetTrans(Tween.TransitionType.Linear);

				tween.TweenCallback(Callable.From(Signal));
			}
		}

		private void Signal() {
			EmitSignal(SignalName.GunReady, 0.5f * Mathf.Pi - _gun.Rotation, GetHeadPosition());
		}

		[Signal]
		public delegate void GunReadyEventHandler(float angleRad, Vector2 headPosition);
	}

}

