using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Zenitka.Scripts._2D.Projectiles;

namespace Zenitka.Scripts._2D;

public partial class Main2D : Node2D
{
	private PackedScene _targetScene;
	private PackedScene _bulletScene;
	private PackedScene _rocketTargetScene;
	private PackedScene _rocketAmmoScene;

	private Marker2D _rocketCannon;
	private Node2D _anchor1;
	private Node2D _anchor2;
	private Timer _targetSpawnTimer;

	private Label _ammoLabel;
	private Label _detectedLabel;
	private Label _destroyedLabel;

	private PBodyProps _normalTargetProps;
	private PBodyProps _rocketTargetProps;

	private List<IWeapon> _weapons = [];

	public override void _Ready()
	{
		_targetScene = GD.Load<PackedScene>("res://Prefabs/2D/Target.tscn");
		_rocketTargetScene = GD.Load<PackedScene>("res://Prefabs/2D/RocketTarget.tscn");

		_weapons =
		[
			GetNode<Cannon>("Cannon1")!,
			GetNode<Cannon>("Cannon2")!,
			//GetNode<RocketLauncher>("RocketLauncher")!
		];

		_anchor1 = GetNode<Node2D>("Anchor");
		_anchor2 = GetNode<Node2D>("Anchor2");

		_ammoLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/UsedAmmo");
		_detectedLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/DetectedTargets");
		_destroyedLabel = GetNode<Label>("CanvasLayer/Statistics/ColorRect/DestroyedTargets");
		
		_targetSpawnTimer = GetNode<Timer>("TargetSpawnTimer");

		Settings.Settings2D.OnSettingsChanged += LoadSettings;
		LoadSettings();
	}

	private void LoadSettings()
	{
		_normalTargetProps = new PBodyProps
		{
			ConstantAcceleration = Settings.Settings2D.GravityVector,
			QDrag = Settings.Settings2D.DefaultTarget.QDrag,
			Mass = Settings.Settings2D.DefaultTarget.Mass,
		};
		
		_rocketTargetProps = new PBodyProps
		{
			ConstantAcceleration = Settings.Settings2D.GravityVector,
			QDrag = Settings.Settings2D.RocketTarget.QDrag,
			Mass = Settings.Settings2D.RocketTarget.BaseMass + Settings.Settings2D.RocketTarget.FuelMass,
			MainEThrust = Settings.Settings2D.RocketTarget.MainEThrust
		};

		_targetSpawnTimer.WaitTime = 3f;
	}

	// public override void _Process(double delta)
	// {
	//     var roc = GetNode<Node2D>("RocketCannon");
	//     var def = GetNode<Node2D>("Cannon");
	//
	//     if (Settings.Settings2D.IsNotDefaultGun && roc.Visible == false)
	//     {
	//         roc.Show();
	//         def.Hide();
	//     }
	//
	//     if (!Settings.Settings2D.IsNotDefaultGun && roc.Visible == true)
	//     {
	//         def.Show();
	//         roc.Hide();
	//     }
	// }

	private void OnTargetSpawnTimerTimeout()
	{
		var scene = Settings.Settings2D.IsNotDefaultTarget ? _rocketTargetScene : _targetScene;
		var props = Settings.Settings2D.IsNotDefaultTarget ? _rocketTargetProps.Copied() : _normalTargetProps;

		var state = CreateTargetState(Settings.Settings2D.IsNotDefaultTarget
			? Settings.Settings2D.RocketTarget.StartVelocity
			: Settings.Settings2D.DefaultTarget.Velocity);

		var target = (scene.Instantiate() as BallisticBody)!;
		target.Reset(props, state);

		AddChild(target);

		_detectedLabel.Text = (int.Parse(_detectedLabel.Text) + 1).ToString();

		// if (Settings.Settings2D.IsNotDefaultGun)
		// {
		// 	var rocket = (_rocketAmmoScene.Instantiate() as Rocket)!;
		//
		// 	rocket.GlobalPosition = _rocketCannon.GlobalPosition;
		// 	rocket.FollowTarget = target;
		//
		// 	AddChild(rocket);
		// 	_ammoLabel.Text = (int.Parse(_ammoLabel.Text) + 1).ToString();
		//
		// 	return;
		// }

		OnTargetDetected(target);
	}

	private void OnTargetDetected(BallisticBody target)
	{
		WeaponCallback callback = (ammoUsed, targetsHit) =>
		{
			_ammoLabel.Text = (int.Parse(_ammoLabel.Text) + ammoUsed).ToString();
			_destroyedLabel.Text = (int.Parse(_destroyedLabel.Text) + targetsHit).ToString();
		};

		var allBusy = true;

		foreach (var weapon in _weapons.Where(weapon => !weapon.IsBusy))
		{
			weapon.OnTargetDetected(target, callback);
			allBusy = false;
		}

		if (allBusy && _weapons.Count > 0)
		{
			_weapons[Utils.Rng.Next(_weapons.Count - 1)].OnTargetDetected(target, callback);
		}
	}

	private PBodyState CreateTargetState(float speed)
	{
		Random rand = new Random();

		bool kind = rand.Next(2) == 0;
		var angle = Utils.RandRange(-Mathf.Pi / 24f, Mathf.Pi / 6f);

		Vector2 pos, vel;

		if (kind)
		{
			pos = new Vector2(_anchor1.GlobalPosition.X, Utils.RandRange(_anchor1.GlobalPosition.Y, -1000f));
			vel = Vector2.FromAngle(angle);
		}
		else
		{
			pos = new Vector2(_anchor2.GlobalPosition.X, Utils.RandRange(_anchor2.GlobalPosition.Y, -1000f));
			vel = Vector2.FromAngle(Mathf.Pi - angle);
		}

		return new PBodyState(pos, vel * speed);
	}

	private void SettingsButton()
	{
		var settingsButtonAnimation = GetNode<AnimationPlayer>("CanvasLayer/Button2/AnimationPlayer");
		var settingsPanel = GetNode<Control>("CanvasLayer/SettingsPanel");
		if (!settingsPanel.Visible)
		{
			settingsPanel.Show();
			var animation = GetNode<AnimationPlayer>("CanvasLayer/SettingsPanel/Animation");
			animation.Play("in");
			settingsButtonAnimation.Play("in");
		}
		else
		{
			// GD.Print("out");
			var animation = GetNode<AnimationPlayer>("CanvasLayer/SettingsPanel/Animation");
			animation.Play("out");
			settingsButtonAnimation.Play("out");
		}
	}
}
