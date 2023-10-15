using System;
using Godot;


namespace Zenitka.Scripts._2D
{
	public partial class Cannon : Node2D
	{

		public float GunRotationSpeed = 1.0f;
		
		private Sprite2D _gun;
		private Node2D _head;

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

		public void RotateToAndSignal(float targetAngle, float time) {
			targetAngle = 0.5f * Mathf.Pi - Mathf.PosMod(targetAngle, 2f * Mathf.Pi);
			var curRot = Mathf.PosMod(Rotation, 2f * Mathf.Pi);
			var diff = Mathf.Abs(curRot - targetAngle);

			if (Mathf.IsEqualApprox(Rotation, targetAngle, 0.001f))
				Signal(time);
			else {
				var tween = CreateTween();

				tween.TweenProperty(_gun, "rotation", targetAngle, diff / GunRotationSpeed)
					.SetTrans(Tween.TransitionType.Linear);

				tween.TweenCallback(Callable.From(()=>Signal(time - (float)tween.GetTotalElapsedTime())));
			}
		}

		private void Signal(float time) {
			EmitSignal(SignalName.GunReady, 0.5f * Mathf.Pi - _gun.Rotation, GetHeadPosition(), time);
		}

		[Signal]
		public delegate void GunReadyEventHandler(float angleRad, Vector2 headPosition);
	}

}

