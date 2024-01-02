using System.Collections.Generic;
using Godot;

namespace Zenitka.Scripts._3D
{
	public readonly struct ProjectileConfig
	{
		public readonly float Speed, Acceleration, SelfPropellingAcceleration, LinearDrag, Mass;

		public ProjectileConfig(float velocity,
								float acceleration,
								float selfPropellingAcceleration,
								float linearDrag,
								float mass)
		{
			Speed = velocity;
			Acceleration = acceleration;
			SelfPropellingAcceleration = selfPropellingAcceleration;
			LinearDrag = linearDrag;
			Mass = mass;
		}
	}

	public class CannonState
	{
		public Vector3 VOrigin, HOrigin;
		public Vector3[] BulletPos;

		public float HRotSpeed, VRotSpeed, HAngle, VAngle;
		public ProjectileConfig Projectile;

		public CannonState(
			Vector3 vOrigin,
			Vector3 hOrigin,
			Vector3[] bulletPos,
			float hAngle,
			float vAngle,
			float hRotSpeed,
			float vRotSpeed,
			ProjectileConfig projectile)
		{
			VOrigin = vOrigin;
			HOrigin = hOrigin;
			BulletPos = bulletPos;
			HAngle = hAngle;
			VAngle = vAngle;
			HRotSpeed = hRotSpeed;
			VRotSpeed = vRotSpeed;
			Projectile = projectile;
		}

		public BodyState CreateProjectile(int id, float hAngle, float vAngle, in Vector3 gravity)
		{
			var transform = MathUtils.Rotated(HOrigin, Vector3.Up, -hAngle) * MathUtils.Rotated(VOrigin, Vector3.Back, vAngle);

			return new BodyState(
				transform * BulletPos[id],
				transform * (BulletPos[id] + Projectile.Speed * Vector3.Right) - transform * BulletPos[id],
				transform * (BulletPos[id] + Projectile.Acceleration * Vector3.Right) + gravity - transform * BulletPos[id],
				Projectile.LinearDrag,
				Projectile.SelfPropellingAcceleration,
				Projectile.Mass);
		}
	}

	public partial class Cannon : StaticBody3D, IWeapon
	{
		private PackedScene _bulletScene;

		private Node3D _centralConstruction;
		private Node3D _baseCannonPart;
		private Node3D _bulletSpawnLocation0;
		private Node3D _bulletSpawnLocation1;

		public Vector3 BulletPos0 => _bulletSpawnLocation0.GlobalPosition;
		public Vector3 BulletPos1 => _bulletSpawnLocation1.GlobalPosition;

		private CannonState _state;
		public CannonState State => _state;

		private Vector3 _gravity => Vector3.Down * Settings.Settings3D.Gravity;

		public override void _Ready()
		{
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/3D/Bullet.tscn");

			_centralConstruction = GetNode<Node3D>("AntiAir/Central Constuction");
			_baseCannonPart = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part");
			_bulletSpawnLocation0 = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part/BulletSpawn1");
			_bulletSpawnLocation1 = GetNode<Node3D>("AntiAir/Central Constuction/Base canon part/BulletSpawn2");

			_state = new CannonState(
				_baseCannonPart.GlobalPosition,
				_centralConstruction.GlobalPosition,
				new Vector3[] { BulletPos0, BulletPos1 },
				0f,
				0f,
				Settings.Settings3D.DefaultGun.HRotSpeed,
				Settings.Settings3D.DefaultGun.VRotSpeed,
				new ProjectileConfig(
					Settings.Settings3D.DefaultGun.BulletSpeed,
					0f,
					0f,
					Settings.Settings3D.DefaultGun.AirResistance,
					Settings.Settings3D.DefaultGun.BulletMass
				)
			);
		}

		public void OnTargetDetected(BallisticBody target, WeaponStatisticsCallback callback) {
			var aimResult = new Solver3D(State, 0, target.State, _gravity, new SolverOptions()).Aim();
			Fire(0, aimResult.HAngle, aimResult.VAngle, aimResult.ColTime, callback);
		}

		private void Fire(int refBulletId, float hAngle, float vAngle, float collisionTime, WeaponStatisticsCallback callback)
		{
			var projectile = _state.CreateProjectile(refBulletId, hAngle, vAngle, _gravity);

			var projectile1 = projectile;
			projectile.Position += BulletPos1 - BulletPos0;

			var hTween = _centralConstruction.CreateTween();
			var targetQuartenion = new Quaternion(Vector3.Up, -hAngle);

			float duration = MathUtils.AngleDiff(hAngle, _state.HAngle) / _state.HRotSpeed;
			hTween.TweenProperty(_centralConstruction, "quaternion", targetQuartenion, duration);

			var vTween = hTween.Chain();
			targetQuartenion = new Quaternion(Vector3.Forward, Mathf.Pi / 2f - vAngle);

			duration = MathUtils.AngleDiff(vAngle, _state.VAngle) / _state.VRotSpeed;
			vTween.TweenProperty(_baseCannonPart, "quaternion", targetQuartenion, duration);

			vTween.TweenCallback(Callable.From(() =>
			{
				_state.HAngle = hAngle;
				_state.VAngle = vAngle;

				projectile.Position = BulletPos0;
				projectile1.Position = BulletPos1;

				Fire(collisionTime, new BodyState[] { projectile, projectile1 }, callback);
			}));
		}

		private void Fire(float collisionTime, BodyState[] projectiles, WeaponStatisticsCallback callback)
		{
			bool hit = false;
			int missed = 0;

			foreach (var projectile in projectiles) {
				var bullet = _bulletScene.Instantiate() as Bullet;
				bullet.State = projectile;
				bullet.Lifespan = collisionTime + 1f;
				GetParent().AddChild(bullet);

				bullet.OnExploded += (target) => {
					if (target != null && !hit) {
						callback.Invoke(projectiles.Length, 1);
						hit = true;
					} else if (target == null && !hit) {
						if (missed >= projectiles.Length)
							callback.Invoke(projectiles.Length, 0);
						else
							++missed;
					}
				};
			}
		}
	}
}
