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

		public Vector3 BulletSpawnPosition => (BulletSpawnPosition0 + BulletSpawnPosition1) / 2f;

		public Vector3 VOrigin => _baseCannonPart.GlobalPosition;
		public Vector3 HOrigin => _centralConstruction.GlobalPosition;

		public float HAngle { get; set; } = 0f;
		public float VAngle { get; set; } = 0f;

		public float HRotSpeed { get; set; } = 3f;
		public float VRotSpeed { get; set; } = 3f;

		public Vector3 Origin => _baseCannonPart.GlobalPosition;
		public float BarrelSize = 0f;

		public event CannonAimed OnAimed;

		public override void _Ready()
		{
			_centralConstruction = GetNode<Node3D>("AntiAir/Central Constuction");
			_baseCannonPart = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part");
			_bulletSpawnLocation0 = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part/BulletSpawn1");
			_bulletSpawnLocation1 = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part/BulletSpawn2");

			BarrelSize = (BulletSpawnPosition - Origin).Length();

			HAngle = -_centralConstruction.Rotation.Y;
			VAngle = _baseCannonPart.Rotation.Z + Mathf.Pi / 2f;
		}

		public void Fire(float hAngle, float vAngle, BodyState3D projectile, float collisionTime)
		{
			var hTween = _centralConstruction.CreateTween();
			var targetQuartenion = new Quaternion(Vector3.Up, -hAngle);

			hTween.TweenProperty(_centralConstruction, "quaternion", targetQuartenion, MathUtils.AngleDiff(hAngle, HAngle) / HRotSpeed);

			var vTween = hTween.Chain();
			targetQuartenion = new Quaternion(Vector3.Forward, Mathf.Pi / 2f - vAngle);

			vTween.TweenProperty(_baseCannonPart, "quaternion", targetQuartenion, MathUtils.AngleDiff(vAngle, VAngle) / VRotSpeed);

			var projectile1 = projectile;
			projectile1.Position += BulletSpawnPosition1 - BulletSpawnPosition0;

			vTween.TweenCallback(Callable.From(() => {
				HAngle = hAngle;
				VAngle = vAngle;

				OnAimed?.Invoke(projectile, projectile1, collisionTime);
			}));
		}
	}
}
