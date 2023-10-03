using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Main3D : Node3D
	{
		private static float BULLET_SPEED = 10f;
		private static float TARGET_SPEED = 10f;
		private static float TARGET_SPAWN_RADIUS = 15f;

		private PackedScene _targetScene;
		private PackedScene _bulletScene;

		private Cannon _cannon;

		private static Random _rng = new Random();

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");

			_targetScene = GD.Load<PackedScene>("res://Prefabs/3D/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/3D/Bullet.tscn");
		}
		
		private void OnCannonAimed()
		{
			var bullet = _bulletScene.Instantiate() as Bullet;
			AddChild(bullet);
			
			bullet.GlobalPosition = _cannon.GetBulletSpawnPosition();
			bullet.LinearVelocity = _cannon.GetBulletSpawnPosition().Normalized() * BULLET_SPEED;
			bullet.GravityScale = 0f;
			bullet.GlobalRotation = _cannon.GlobalRotation;
			bullet.SetLifespan(10f);
			
			ToSignal(bullet, Bullet.SignalName.Exploded).OnCompleted(() => {
				bullet.QueueFree();
			});
		}
		
		private void OnTargetSpawnTimerTimeout()
		{
			var target = _targetScene.Instantiate() as Target;

			bool kind = _rng.Next(2) == 0;
			GD.Print(kind);

			var startPos = GenerateTargetSpawnlocation(kind);
			var endPos = GenerateTargetSpawnlocation(!kind);
			AddChild(target);

			target.Position = startPos;
			target.LinearVelocity = (endPos - startPos).Normalized() * TARGET_SPEED;
			target.GravityScale = 0f;

			target.SetDetectionRange(50f);

			ToSignal(target, Target.SignalName.WentWithinRange3D).OnCompleted((Action)(() => {
				 var dir = MathUtils.CalculateCannonDirection3D(
					_cannon.GetBarrelSize(),
					BULLET_SPEED,
					target.GlobalPosition,
					target.LinearVelocity
				);

				if (dir.LengthSquared() > 0.001f)
					_cannon.RotateToAndSignal(dir.Normalized());
			}));
		}
		
		private Vector3 GenerateTargetSpawnlocation(bool kind) {
			var x = MathUtils.RandRange(0f, 1f);

			if (kind)
				x = -x;

			var y = MathUtils.RandRange(0f, Mathf.Sqrt(1f - x * x));
			var z = Mathf.Sqrt(1f - x * x - y * y);

			if (_rng.Next(2) == 0)
				z = -z;
			
			return TARGET_SPAWN_RADIUS * new Vector3(x, y, z).Normalized();
		}
		
		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
