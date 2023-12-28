using Godot;
using System;

namespace Zenitka.Scripts._3D
{
	public partial class Main3D : Node3D
	{
		private const float TARGET_SPAWN_RADIUS = 100f;

		private PackedScene _targetScene;
		private PackedScene _bulletScene;
		private Label _destroyedLabel;
		//private PackedScene _explosionScene;

		private Label _ammoLabel;
		private Label _detectedLabel;

		private Cannon _cannon;

		private static Random _rng = new Random();

		private CannonState3D _cannonState;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_cannon = GetNode<Cannon>("Cannon");
			_cannon.OnAimed += OnCannonAimed;

			_ammoLabel = GetNode<Label>("Statistics/ColorRect/UsedAmmo");
			_detectedLabel = GetNode<Label>("Statistics/ColorRect/DetectedTargets");
			_destroyedLabel = GetNode<Label>("Statistics/ColorRect/DestroyedTargets");

			_targetScene = GD.Load<PackedScene>("res://Prefabs/3D/Target.tscn");
			_bulletScene = GD.Load<PackedScene>("res://Prefabs/3D/Bullet.tscn");
			//_explosionScene = GD.Load<PackedScene>("res://Prefabs/3D/Explosion.tscn");

			_cannonState = new CannonState3D(
				_cannon.VOrigin,
				_cannon.HOrigin,
				_cannon.BulletSpawnPosition0,
				_cannon.HAngle,
				_cannon.VAngle,
				_cannon.BarrelSize,
				_cannon.HRotSpeed,
				_cannon.VRotSpeed,
				new ProjectileConfig3D(
					Settings.Settings3D.DefaultGun.BulletSpeed,
					0f,
					0f,
					Settings.Settings3D.DefaultGun.AirResistance,
					Settings.Settings3D.DefaultGun.BulletMass
				)
			);
		}

		public override void _EnterTree()
		{
			base._EnterTree();
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		public override void _ExitTree()
		{
			base._ExitTree();
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		public override void _UnhandledInput(InputEvent @event)
		{
			base._UnhandledInput(@event);

			if (@event is InputEventMouseButton && !GetTree().Paused) {
				var mouseEvent = @event as InputEventMouseButton;

				if (mouseEvent.ButtonIndex == MouseButton.Left)
					Input.MouseMode = Input.MouseModeEnum.Captured;
				else
					Input.MouseMode = Input.MouseModeEnum.Visible;
			}
		}

		private void OnCannonAimed(BodyState3D projectile1, BodyState3D projectile2, float collisionTime)
		{
			//GD.Print("aimed");

			var bullet = _bulletScene.Instantiate() as Bullet;
			AddChild(bullet);

			bullet.State = projectile1;

			// var tmp = bullet.State;
			// tmp.Position = _cannon.BulletSpawnPosition0;
			// bullet.State = tmp;

			GD.Print("diff: ", (bullet.State.Position - _cannon.BulletSpawnPosition0).Length());

			bullet.OnExploded += (target) => {
				if (target != null && !target.IsQueuedForDeletion()) {
					GD.Print("Hit");
					_destroyedLabel.Text = (Int32.Parse(_destroyedLabel.Text) + 1).ToString();
					target.QueueFree();
				} else if (target == null)
					GD.Print("Missed.");
			};

			var bullet1 = _bulletScene.Instantiate() as Bullet;

			AddChild(bullet1);

			bullet1.State = projectile2;

			// tmp = bullet1.State;
			// tmp.Position = _cannon.BulletSpawnPosition1;
			// bullet1.State = tmp;

			bullet1.OnExploded += (target) => {
				if (target != null && !target.IsQueuedForDeletion()) {
					GD.Print("Hit");
					_destroyedLabel.Text = (Int32.Parse(_destroyedLabel.Text) + 1).ToString();
					target.QueueFree();
				} else if (target == null)
					GD.Print("Missed.");
			};

			_ammoLabel.Text = (Int32.Parse(_ammoLabel.Text) + 2).ToString();
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var kind = _rng.Next(2) == 0;

			var startPos = GenerateTargetSpawnLocation(kind);
			var endPos = GenerateTargetSpawnLocation(!kind);

			var targetState = new BodyState3D(
				startPos,
				Settings.Settings3D.DefaultTarget.Velocity * (endPos - startPos).Normalized(),
				Vector3.Down * Settings.Settings3D.Gravity,
				Settings.Settings3D.DefaultTarget.AirResistance,
				0f,
				Settings.Settings3D.DefaultTarget.Mass
			);

			var target = _targetScene.Instantiate() as Target;
			AddChild(target);

			target.State = targetState;

			_detectedLabel.Text = (Int32.Parse(_detectedLabel.Text) + 1).ToString();

			target.CannonPosition = _cannon.GlobalPosition;
			target.CannnonRange = 100f;

			target.OnCannonVisiblityChanged = (visible) =>
			{
				if (visible) {
					_cannonState.VAngle = _cannon.VAngle;
					_cannonState.HAngle = _cannon.HAngle;

					var (hAngle, vAngle, timeOfCollision, projectile) = new Solver3D(
						_cannonState,
						targetState,
						Vector3.Down * Settings.Settings3D.Gravity
					).Aim2();

					_cannon.Fire(hAngle, vAngle, projectile, timeOfCollision);
				}
			};
		}

		private static Vector3 GenerateTargetSpawnLocation(bool kind)
		{
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




