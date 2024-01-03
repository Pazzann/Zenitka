using Godot;
using System;
using System.Collections.Generic;

namespace Zenitka.Scripts._3D
{
	public partial class Main3D : Node3D
	{
		private PackedScene _targetScene;

		private Label _destroyedLabel;
		private Label _ammoLabel;
		private Label _detectedLabel;

		private List<IWeapon> _weapons = new List<IWeapon>();
		private List<Target> _targets = new List<Target>();

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			_targetScene = GD.Load<PackedScene>("res://Prefabs/3D/Target.tscn");

			_ammoLabel = GetNode<Label>("Statistics/ColorRect/UsedAmmo");
			_detectedLabel = GetNode<Label>("Statistics/ColorRect/DetectedTargets");
			_destroyedLabel = GetNode<Label>("Statistics/ColorRect/DestroyedTargets");

			var cannon = GetNode<Cannon>("Cannon");
			var cannon2 = GetNode<Cannon>("Cannon2");
			var rocketLauncher = GetNode<RocketLauncher>("RocketLauncher");

			_weapons.Add(cannon);
			_weapons.Add(cannon2);
			_weapons.Add(rocketLauncher);
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

		private void OnTargetSpawnTimerTimeout()
		{
			var pos = 100f * Utils.OrientPlane(Utils.RandUnitSphere(), Vector3.Up);
			var vel = Settings.Settings3D.DefaultTarget.Velocity * Utils.OrientPlane(Utils.RandUnitSphere(), -pos);

			var targetState = new BodyState(
				pos,
				vel,
				Vector3.Down * Settings.Settings3D.Gravity,
				Settings.Settings3D.DefaultTarget.AirResistance,
				0f,
				Settings.Settings3D.DefaultTarget.Mass
			);

			var target = _targetScene.Instantiate() as Target;
			target.State = targetState;
			_targets.Add(target);
			AddChild(target);

			_detectedLabel.Text = (int.Parse(_detectedLabel.Text) + 1).ToString();
			OnTargetDetected(target);
		}

		private void OnTargetDetected(Target target) {
			foreach (var weapon in _weapons)
				weapon.OnTargetDetected(target, (ammoUsed, targetsHit) => {
					_destroyedLabel.Text = (int.Parse(_destroyedLabel.Text) + targetsHit).ToString();
					_ammoLabel.Text = (int.Parse(_ammoLabel.Text) + ammoUsed).ToString();
				});
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
