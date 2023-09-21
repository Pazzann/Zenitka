using Godot;
using Microsoft.VisualBasic;
using System;
using System.Net;

namespace Zenitka.Scripts._2D 
{
	public partial class Main2D : Node2D
	{
		private static float BARREL_LENGTH = 200f;
		private static float MUZZLE_SPEED = 200f;
		private static float TARGET_SPEED = 200f;

		private PackedScene _targetScene;
		private PackedScene _bulletScene;

		private Cannon _cannon;

		private static Random _rng = new Random();

		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");

			_targetScene = GD.Load<PackedScene>("res://Prefabs/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/Bullet.tscn");
		}

		private void OnCannonGunReady(double angleRad)
		{
			float angleRadF = (float) angleRad;

			var bullet = _bulletScene.Instantiate() as Bullet;

			bullet.Rotate(Mathf.Pi * 0.5f - angleRadF);

			bullet.GlobalPosition = _cannon.GetHeadPosition(); 
			bullet.LinearVelocity = Vector2.FromAngle((float) - angleRadF) * MUZZLE_SPEED;
			bullet.GravityScale = 0f;

			bullet.SetLifespan(10f);
			AddChild(bullet);

			ToSignal(bullet, Bullet.SignalName.SelfDestroyed).OnCompleted(() => {
				bullet.QueueFree();
			});
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var target = _targetScene.Instantiate() as Target;

			bool kind = _rng.Next(2) == 0;

			var startPos = GenerateTargetSpawnlocation(kind);
			var endPos = GenerateTargetSpawnlocation(!kind);

			// TODO: use actual object size
			target.GlobalPosition = startPos - new Vector2(20f, 20f);
			
			target.LinearVelocity = (endPos - startPos).Normalized() * TARGET_SPEED;

			target.GravityScale = 0f;

			ToSignal(target, Target.SignalName.WentWithinRange).OnCompleted(() => {
				float a = MathUtils.CalculateFiringAngle(
					BARREL_LENGTH,
					MUZZLE_SPEED,
					target.GlobalPosition,
					target.LinearVelocity
				);

				GD.Print("cannon angle: ", a);

				if (a <= 1.01f * Mathf.Pi && a >= -0.01f) {
					_cannon.RotateToAndSignal(a);
				}
			});

			AddChild(target);
		}

		private Vector2 GenerateTargetSpawnlocation(bool kind) {
			var rect = GetWindowRect();

			var baseX = rect.Position.X;
			
			if (kind)
				baseX += rect.Size.X * 1.1f;
			else
				baseX -= rect.Size.X * 0.1f;

			var y = MathUtils.RandRange(rect.Position.Y, rect.Position.Y + rect.Size.Y);

			return new Vector2(baseX, y);
		}

		private Rect2 GetWindowRect() {
			var camera = GetViewport().GetCamera2D();
			var viewportSize = GetViewportRect().Size;

			return new Rect2(camera.GetScreenCenterPosition() - viewportSize * 0.5f, viewportSize);
		}
	}
}
