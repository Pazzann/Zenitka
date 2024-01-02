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
		private PackedScene _explosionScene;

		private Label _ammoLabel;
		private Label _detectedLabel;

		private Cannon _cannon;

		private static Random _rng = new Random();

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
			_explosionScene = GD.Load<PackedScene>("res://Prefabs/3D/Explosion.tscn");
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

		private void OnCannonAimed(float collisionTime, BodyState[] projectiles)
		{
			int missCount = 0;
			bool hit = false;

			foreach (var projectile in projectiles) {
				var bullet = _bulletScene.Instantiate() as Bullet;
				AddChild(bullet);

				bullet.State = projectile;

				bullet.OnExploded += (target) => {
					if (target != null && !hit && !target.IsQueuedForDeletion()) {
						GD.Print("Hit");
						_destroyedLabel.Text = (int.Parse(_destroyedLabel.Text) + 1).ToString();
						hit = true;

						if (target is Target)
							(target as Target).Explode();
						else
							target.QueueFree();

					} else if (target == null && !hit && missCount == projectiles.Length - 1)
						GD.Print("Missed.");
					else if (target == null && !hit)
						++missCount;
				};
			}

			_ammoLabel.Text = (int.Parse(_ammoLabel.Text) + projectiles.Length).ToString();
		}

		private void OnTargetSpawnTimerTimeout()
		{
			var kind = _rng.Next(2) == 0;

			var startPos = GenerateTargetSpawnLocation(kind);
			var endPos = GenerateTargetSpawnLocation(!kind);

			var targetState = new BodyState(
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

			_detectedLabel.Text = (int.Parse(_detectedLabel.Text) + 1).ToString();

			target.CannonPosition = _cannon.GlobalPosition;
			target.CannnonRange = 100f;

			target.OnCannonVisiblityChanged = (visible) =>
			{
				if (visible) {
					var result = new Solver3D(
						_cannon.State,
						0,
						targetState,
						Vector3.Down * Settings.Settings3D.Gravity,
						new SolverOptions()
					).Aim();

					_cannon.Fire(0, result.HAngle, result.VAngle, result.ColTime);
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
				var animation = GetNode<AnimationPlayer>("SettingsPanel3D/Animation");
				animation.Play("out");
				settingsButtonAnimation.Play("out");
			}

		}
	}

}




