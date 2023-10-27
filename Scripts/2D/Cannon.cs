using System;
using Godot;


namespace Zenitka.Scripts._2D
{
	public delegate void CannonAimedEventHandler(float angleRad, float timeOfCollision, Vector2 headPosition, Target target);

	public partial class Cannon : Node2D
	{

		private Sprite2D _gun;
		private Node2D _head;

		public event CannonAimedEventHandler OnCannonAimed;

		public override void _Ready()
		{
			_gun = GetNode<Sprite2D>("Gun");
			_head = GetNode<Node2D>("Gun/Bullet1");
		}

		public Vector2 GetHeadPosition()
		{
			return _head.GlobalPosition;
		}

		public float GetAngle()
		{
			return 0.5f * Mathf.Pi - _gun.Rotation;
		}

		public void RotateToAndSignal(float targetAngle, float timeOfCollision, Target target)
		{
			targetAngle = 0.5f * Mathf.Pi - Mathf.PosMod(targetAngle, 2f * Mathf.Pi);

			var curRot = _gun.Rotation;
			var diff = Mathf.Abs(curRot - targetAngle);

			if (Mathf.IsEqualApprox(Rotation, targetAngle, 0.001f))
				Signal(timeOfCollision, target);
			else
			{
				var tween = CreateTween();

				tween.TweenProperty(_gun, "rotation", targetAngle, diff / Settings.Settings2D.DefaultGun.AngularVelocity)
					.SetTrans(Tween.TransitionType.Linear);

				tween.TweenCallback(Callable.From(() => Signal(timeOfCollision, target)));
			}
		}

		private void Signal(float timeOfCollision, Target target)
		{
			OnCannonAimed?.Invoke(0.5f * Mathf.Pi - _gun.Rotation, timeOfCollision, GetHeadPosition(), target);
		}
	}

}

