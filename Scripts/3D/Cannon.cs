using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public delegate void CannonAimed(BodyState3D projectile0, BodyState3D projectile1, float collisionTime);

	public partial class Cannon : CharacterBody3D
	{
		private Node3D _centralConstruction;
		private Node3D _baseCannonPart;
		private Node3D _bulletSpawnLocation0;
		private Node3D _bulletSpawnLocation1;

		public Vector3 BulletSpawnPosition0 => _bulletSpawnLocation0.GlobalPosition;
		public Vector3 BulletSpawnPosition1 => _bulletSpawnLocation1.GlobalPosition;

		public Vector3 BulletSpawnPosition => BulletSpawnPosition0;

		public Vector3 AimDirection
		{
			get { return (BulletSpawnPosition - GlobalPosition).Normalized(); }
		}

		public float AngularRotationSpeed { get; set; } = 3f;

		public Vector3 Origin => _centralConstruction.GlobalPosition;
		public float BarrelSize = 0f;

		public event CannonAimed OnAimed;

		public override void _Ready()
		{
			_centralConstruction = GetNode<Node3D>("AntiAir/Central Constuction");
			_baseCannonPart = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part");
			_bulletSpawnLocation0 = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part/BulletSpawn1");
			_bulletSpawnLocation1 = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part/BulletSpawn2");

			BarrelSize = (BulletSpawnPosition - Origin).Length();
		}

		public void RotateToAndSignal(Vector3 targetDirection, BodyState3D projectile, float collisionTime)
		{
			var targetDirectionProj = targetDirection - Vector3.Up * targetDirection.Dot(Vector3.Up);
			var aimDirectionProj = AimDirection - Vector3.Up * AimDirection.Dot(Vector3.Up);

			var angleY = -SignedAngle(aimDirectionProj, targetDirectionProj, Vector3.Up);
			var animDuration1 = Mathf.Abs(angleY) / AngularRotationSpeed;
			var targetRotation1 = new Vector3(0f, _centralConstruction.Rotation.Y + angleY, 0f);

			var yTween = _centralConstruction.CreateTween();

			yTween.TweenProperty(_centralConstruction, "rotation", targetRotation1, animDuration1)
				.SetEase(Tween.EaseType.In)
				.SetTrans(Tween.TransitionType.Linear);

			var axis2 = targetDirectionProj.Cross(targetDirection);
			var angle2 = -SignedAngle(targetDirectionProj, targetDirection, axis2);
			var targetRotation2 = new Vector3(0f, 0f, angle2 - Mathf.Pi / 2f);
			float animDuration2 = Mathf.Abs(angle2) / AngularRotationSpeed;

			var xzTween = yTween.Chain();

			xzTween.TweenProperty(_baseCannonPart, "rotation", targetRotation2, animDuration2)
				.SetEase(Tween.EaseType.In)
				.SetTrans(Tween.TransitionType.Linear);

			var projectile1 = projectile;
			projectile1.Position += BulletSpawnPosition1 - BulletSpawnPosition0;

			xzTween.TweenCallback(Callable.From(() => OnAimed?.Invoke(projectile, projectile1, collisionTime)));
		}

		private static float SignedAngle(Vector3 a, Vector3 b, Vector3 normal)
		{
			var angle = a.AngleTo(b);

			if (a.Cross(b).Dot(normal) > 0)
				angle = -angle;

			return angle;
		}
	}
}
