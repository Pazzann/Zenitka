using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Main3D : Node3D
	{
		// private static float BULLET_SPEED = 10f;
		// private static float TARGET_SPEED = 10f;
		private const float TARGET_SPAWN_RADIUS = 15f;

		private const float TARGET_SPEED = 50f;
		private const float TARGET_LINEAR_DRAG = 0.05f;
		private const float TARGET_MASS = 1f;

		private const float BULLET_SPEED = 100f;
		private const float BULLET_LINEAR_DRAG = 0.05f;
		private const float BULLET_MASS = 1f;

		private static readonly Vector3 GRAVITY = new Vector3(0f, -9.8f, 0f);

		private PackedScene _targetScene;
		private PackedScene _bulletScene;

		private Cannon _cannon;

		private static Random _rng = new Random();

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_cannon.OnAimed += OnCannonAimed;

			_targetScene = GD.Load<PackedScene>("res://Prefabs/3D/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/3D/Bullet.tscn");
		}
		
		private void OnCannonAimed(ParticleState3D projectile, float collisionTime)
		{
			//GD.Print("aimed");

			var bullet = _bulletScene.Instantiate() as Bullet;
			AddChild(bullet);

			bullet.State = projectile;
			bullet.Lifespan = 10f;
			
			bullet.OnExploded += (target) => {
				GD.Print("Hit");
			};
		}
		
		private void OnTargetSpawnTimerTimeout()
		{
			var kind = _rng.Next(2) == 0;

			var startPos = GenerateTargetSpawnLocation(kind);
			var endPos = GenerateTargetSpawnLocation(!kind);

			var targetState = new ParticleState3D(
				startPos,
				TARGET_SPEED * (endPos - startPos).Normalized(),
				Vector3.Zero,
				TARGET_LINEAR_DRAG
			);

			var target = _targetScene.Instantiate() as Target;
			AddChild(target);

			target.State = targetState;

			target.CannonPosition = _cannon.GlobalPosition;
			target.CannnonRange = 100f;
		
			target.OnCannonVisiblityChanged = (visible) => {
				if (visible) {
					var (dir, timeOfCollision, projectile) = new Solver3D(
						new CannonState3D(
							_cannon.BulletSpawnPosition,
							_cannon.AimDirection,
							_cannon.AngularRotationSpeed,
							BULLET_SPEED,
							0f,
							BULLET_LINEAR_DRAG
						),
						targetState,
						new Vector3(0f, -9.8f, 0f)
					).Aim();

					if (dir.LengthSquared() > 0.001f)
						_cannon.RotateToAndSignal(dir.Normalized(), projectile, timeOfCollision);
				}
			};
		}
		
		private static Vector3 GenerateTargetSpawnLocation(bool kind) {
			var x = MathUtils.RandRange(0f, 1f);

			if (kind)
				x = -x;

			var y = MathUtils.RandRange(0f, Mathf.Sqrt(1f - x * x));
			var z = Mathf.Sqrt(1f - x * x - y * y);

			if (_rng.Next(2) == 0)
				z = -z;
			
			return TARGET_SPAWN_RADIUS * new Vector3(x, y, z).Normalized();
		}
	}
}
