using System.Collections.Generic;
using Godot;
using Zenitka.Scripts._2D.Projectiles;

namespace Zenitka.Scripts._2D;

public partial class RocketLauncher : StaticBody2D, IWeapon
{
	private PackedScene _rocketScene;
	private Marker2D _rocketPos;

	private readonly Stack<(BallisticBody, WeaponCallback)> _targets = [];

	private PBodyProps _projectileProps;

	public bool IsBusy { get; private set; }

	public override void _Ready()
	{
		_rocketScene = GD.Load<PackedScene>("res://Prefabs/2D/Rocket.tscn")!;
		_rocketPos = GetNode<Marker2D>("RocketPos")!;
		
		LoadSettings();
		Settings.Settings2D.OnSettingsChanged += LoadSettings;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (!IsBusy && _targets.Count > 0)
		{
			var (target, callback) = _targets.Pop();
			ShootTarget(target, callback);
		}
	}
	
	private void LoadSettings()
	{
		_projectileProps = new PBodyProps(
			Settings.Settings2D.RocketGun.RocketMassWithoutFuel + Settings.Settings2D.RocketGun.FuelMass,
			0f,
			Settings.Settings2D.RocketGun.AirResistance,
			Settings.Settings2D.RocketGun.MainEThrust,
			Settings.Settings2D.RocketGun.SideEThrust,
			Settings.Settings2D.GravityVector,
			Vector2.Zero
		);
	}

	public void OnTargetDetected(BallisticBody target, WeaponCallback callback)
	{
		_targets.Push((target, callback));
	}

	private void ShootTarget(BallisticBody target, WeaponCallback callback)
	{
		//IsBusy = true;

		var rocket = (_rocketScene.Instantiate() as Rocket)!;

		rocket.TrackedTarget = target;
		rocket.Rotation = -0.5f * Mathf.Pi;
		rocket.Reset(_projectileProps, new PBodyState(_rocketPos.GlobalPosition, Vector2.Up * Settings.Settings2D.RocketGun.InitialVelocity));
		
		GetParent().AddChild(rocket);
	}
}
