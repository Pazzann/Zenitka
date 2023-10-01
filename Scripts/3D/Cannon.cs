using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Cannon : CharacterBody3D
	{
		private const float ROTATION_SPEED = 20f;

		private Marker3D _bulletSpawnLocation;

		private float _barrelSize = 0f;

		public override void _Ready()
		{
			_bulletSpawnLocation = GetNode<Marker3D>("BulletSpawnLocation");
			_barrelSize = _bulletSpawnLocation.Position.Length();
		}
		
		public Vector3 GetBulletSpawnPosition() {
			return _bulletSpawnLocation.GlobalPosition;
		}

		public float GetBarrelSize() {
			return _barrelSize;
		}
		
		public void RotateToAndSignal(Vector3 targetDirection) {
			var diffAngleCos = targetDirection.Normalized().Dot(GetBulletSpawnPosition().Normalized());
			var diffAngle = Mathf.Acos(diffAngleCos);
			var animDuration = diffAngle / ROTATION_SPEED;

			var targetTransform = GlobalTransform.LookingAt(targetDirection, Vector3.Up);

			var tween = CreateTween();
			
			tween.TweenProperty(this, "global_transform", targetTransform, animDuration)
				.SetEase(Tween.EaseType.In)
				.SetTrans(Tween.TransitionType.Sine);

			tween.TweenCallback(Callable.From(() => {
				EmitSignal(SignalName.Aimed);
			}));
		}

		[Signal]
		public delegate void AimedEventHandler();
	}
}
