using Godot;
using Microsoft.VisualBasic;
using System;
using System.Net;

namespace Zenitka.Scripts._2D 
{
	public partial class Main2D : Node2D
	{
		private static float BARREL_LENGTH = 200f;
		private static float BULLET_SPEED = 1000f;
		private static float TARGET_SPEED = 500f;

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

		private void OnCannonGunReady(double angleRad, Vector2 headPosition)
		{
	
			float angleRadF = (float) angleRad;

			var bullet = _bulletScene.Instantiate() as Bullet;

			bullet.Rotate(Mathf.Pi * 0.5f - angleRadF);

			bullet.GlobalPosition = headPosition; 
			bullet.LinearVelocity = Vector2.FromAngle((float) - angleRadF) * BULLET_SPEED;
			bullet.GravityScale = 1f;

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
				float a = MathUtils.CalculateFiringAngle2(
					_cannon.GetAngle(),
					BARREL_LENGTH,
					10f,
					Vector2.Zero,
					BULLET_SPEED,
					Vector2.Zero,
					new BodyState(
						target.GlobalPosition,
						target.LinearVelocity,
						Vector2.Zero,
						Vector2.Zero
					)
				);

				// float a = MathUtils.CalculateFiringAngle(
				// 	BARREL_LENGTH,
				// 	BULLET_SPEED,
				// 	target.GlobalPosition,
				// 	target.LinearVelocity
				// );

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

			// var rect = GetWindowRect();
			// var r = Mathf.Max(rect.Size.X, rect.Size.Y);

			// var x = MathUtils.RandRange(0f, 1f);

			// if (kind)
			// 	x = -x;

			// var y = Mathf.Sqrt(1f - x * x);

			// if (_rng.Next(2) == 0)
			// 	y = -y;

			// return r * new Vector2(x, y).Normalized() + GetWindowRect().GetCenter();
		}

		private Rect2 GetWindowRect() {
			var camera = GetViewport().GetCamera2D();
			var viewportSize = GetViewportRect().Size;

			return new Rect2(camera.GetScreenCenterPosition() - viewportSize * 0.5f, viewportSize);
		}
	}
}
