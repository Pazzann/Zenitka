using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Main3D : Node3D
	{
		private static float BARREL_LENGTH = 1f;
		private static float MUZZLE_SPEED = 5f;
		private static float TARGET_SPEED = 1f;

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
		
		private void OnCannonGunReady(double angleRadX, double angleRadY)
		{
			float angleRadFX = (float) angleRadX;
			float angleRadFY = (float) angleRadY;
			
			var bullet = _bulletScene.Instantiate() as Bullet;
			AddChild(bullet);
			bullet.RotateX(Mathf.Pi * 0.5f - angleRadFX);
			bullet.RotateY(Mathf.Pi * 0.5f - angleRadFY);
			
			bullet.GlobalPosition = _cannon.GetHeadPosition(); 
			// bullet.LinearVelocity = ;
			
			bullet.GravityScale = 0f;
			
			bullet.SetLifespan(10f);
			
			
			ToSignal(bullet, Bullet.SignalName.SelfDestroyed3D).OnCompleted(() => {
				bullet.QueueFree();
			});
		}
		
		private void OnTargetSpawnTimerTimeout()
		{
			var target = _targetScene.Instantiate() as Target;

			bool kind = _rng.Next(2) == 0;

			var startPos = GenerateTargetSpawnlocation(kind);
			var endPos = GenerateTargetSpawnlocation(!kind);
			AddChild(target);
			// TODO: use actual object size
			target.GlobalPosition = startPos;
			
			target.LinearVelocity = (endPos - startPos).Normalized() * TARGET_SPEED;

			target.GravityScale = 0f;

			var canonPos = _cannon.GetCannonPosition();
			ToSignal(target, Target.SignalName.WentWithinRange3D).OnCompleted(() => {
				 var (a, b) = MathUtils.CalculateFiringAngle3D(
					BARREL_LENGTH,
					MUZZLE_SPEED,
					target.GlobalPosition,
					target.LinearVelocity,
					canonPos
				);

				GD.Print("cannon angle: ", a, " ", b);

				if (a <= 1.01f * Mathf.Pi && a >= -0.01f && b <= 1.01f * Mathf.Pi && b >= -0.01f) {
					_cannon.RotateToAndSignal(a, b);
				}
			});

			
		}
		
		private Vector3 GenerateTargetSpawnlocation(bool kind) {
			var rect = GetWindowRect();
			
			var baseX = rect.Size.X;
			
			if (kind)
				baseX -= rect.Size.X * 0.1f;
			else
				baseX -= rect.Size.X * 0.9f;

			var y = MathUtils.RandRange(0, rect.Size.Y);
			
			var z = MathUtils.RandRange(0, rect.Size.Z);
			
			return new Vector3(baseX, y, z);
		}

		private Aabb GetWindowRect() {
		// 	// var camera = GetViewport().GetCamera2D();
		// 	// var viewportSize = GetViewportRect().Size;
		//
			return new Aabb(Vector3.Zero, new Vector3(20, 20, 20));
		}
		
		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}


