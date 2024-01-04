using System.Collections.Generic;
using Godot;
using Zenitka.Scripts._2D.Projectiles;

namespace Zenitka.Scripts._2D;

public partial class RocketLauncher : StaticBody2D, IWeapon
{
	private PackedScene _rocketScene;
	private Marker2D _rocketPos;

	private Stack<(BallisticBody, WeaponCallback)> _targets = [];

	public bool IsBusy { get; private set; }

	public override void _Ready()
	{
		_rocketScene = GD.Load<PackedScene>("res://Prefabs/2D/Rocket.tscn")!;
		_rocketPos = GetNode<Marker2D>("RocketPos")!;
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

	public void OnTargetDetected(BallisticBody target, WeaponCallback callback)
	{
		_targets.Push((target, callback));
	}

	private void ShootTarget(BallisticBody target, WeaponCallback callback)
	{
		//IsBusy = true;

		var rocket = (_rocketScene.Instantiate() as Rocket)!;

		rocket.FollowTarget = target;
		rocket.GlobalPosition = _rocketPos.GlobalPosition;
		
		GetParent().AddChild(rocket);
	}
}
