using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Main3D : Node3D
	{
		// private static float BULLET_SPEED = 10f;
		// private static float TARGET_SPEED = 10f;
		private const float TARGET_SPAWN_RADIUS = 80f;

		private static readonly Vector3 GRAVITY = new Vector3(0f, 0f, 0f);

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
		
		private void OnCannonAimed(ParticleState3D projectile1, ParticleState3D projectile2, float collisionTime)
		{
			//GD.Print("aimed");

			var bullet = _bulletScene.Instantiate() as Bullet;
			AddChild(bullet);

			bullet.State = projectile1;
			bullet.Lifespan = 10f;
			
			bullet.OnExploded += (target) => {
				if (target != null) {
					GD.Print("Hit");
					target.QueueFree();
				} else {
					GD.Print("Missed.");
				}
			};

			var bullet1 = _bulletScene.Instantiate() as Bullet;
			AddChild(bullet1);

			bullet1.State = projectile2;
			bullet1.Lifespan = 10f;
			
			bullet1.OnExploded += (target) => {
				if (target != null) {
					GD.Print("Hit");
					target.QueueFree();
				} else {
					GD.Print("Missed.");
				}
			};
		}
		
		private void OnTargetSpawnTimerTimeout()
		{
			var kind = _rng.Next(2) == 0;

			var startPos = GenerateTargetSpawnLocation(kind);
			var endPos = GenerateTargetSpawnLocation(!kind);

			var targetState = new ParticleState3D(
				startPos,
				Settings.Settings3D.DefaultTarget.Velocity * (endPos - startPos).Normalized(),
				GRAVITY,
				Settings.Settings3D.DefaultTarget.AirResistance / Settings.Settings3D.DefaultTarget.Mass
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
							_cannon.Origin,
							_cannon.AimDirection,
							_cannon.BarrelSize,
							_cannon.AngularRotationSpeed,
							Settings.Settings3D.DefaultGun.BulletSpeed,
							0f,
							Settings.Settings3D.DefaultGun.AirResistance / Settings.Settings3D.DefaultGun.BulletMass
						),
						targetState,
						GRAVITY
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
		private void SettingsButton()
		{
			var settingsButtonAnimation = GetNode<AnimationPlayer>("Button2/AnimationPlayer");
			var settingsPanel = GetNode<Control>("SettingsPanel3D");
			if (!settingsPanel.Visible)
			{
				settingsPanel.Show();
				var animation = GetNode<AnimationPlayer>("SettingsPanel3D/Animation");
				animation.Play("in");
				settingsButtonAnimation.Play("in");
			}
			else
			{
				// GD.Print("out");
				var animation = GetNode<AnimationPlayer>("SettingsPanel3D/Animation");
				animation.Play("out");
				settingsButtonAnimation.Play("out");


			}

		}
	}
	
}




