using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public delegate void CannonAimed(ParticleState3D projectile, float collisionTime);

	public partial class Cannon : CharacterBody3D
	{	
		private Marker3D _bulletSpawnLocation;

		public Vector3 BulletSpawnPosition
		{
			get { return _bulletSpawnLocation.GlobalPosition; }
		}

		public Vector3 AimDirection
		{
			get { return (BulletSpawnPosition - GlobalPosition).Normalized(); }
		}

		public float BarrelSize { get; private set; }
		public float AngularRotationSpeed { get; set; } = 3f;

		public event CannonAimed OnAimed;

		public override void _Ready()
		{
			_bulletSpawnLocation = GetNode<Marker3D>("BulletSpawnLocation");
			BarrelSize = _bulletSpawnLocation.Position.Length();
		}
		
		public void RotateToAndSignal(Vector3 targetDirection, ParticleState3D projectile, float collisionTime) {
			var diffAngle = targetDirection.AngleTo(AimDirection);
			var animDuration = diffAngle / AngularRotationSpeed;

			var targetTransform = GlobalTransform.LookingAt(targetDirection, Vector3.Up);

			var tween = CreateTween();
			
			tween.TweenProperty(this, "global_transform", targetTransform, animDuration)
				.SetEase(Tween.EaseType.In)
				.SetTrans(Tween.TransitionType.Sine);

			tween.TweenCallback(Callable.From(() => OnAimed?.Invoke(projectile, collisionTime)));
		}

	}
}
