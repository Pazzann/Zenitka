using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Zenitka.Scripts._2D.Projectiles;

namespace Zenitka.Scripts._2D;

public partial class Cannon : StaticBody2D, IWeapon
{
	private PackedScene _bulletScene;

	private Sprite2D _gun;
	private Node2D _bulletPos1;
	private Node2D _bulletPos2;

	private float _rotSpeed;
	private readonly Stack<(BallisticBody, WeaponCallback)> _targets = new();

	public WeaponProjectileProps ProjectileProps { get; private set; }
	public bool IsBusy { get; private set; }

	public override void _Ready()
	{
		_bulletScene = GD.Load<PackedScene>("res://Prefabs/2D/Bullet.tscn");

		_gun = GetNode<Sprite2D>("Body/Gun");
		_bulletPos1 = GetNode<Marker2D>("Body/Gun/Bullet1");
		_bulletPos2 = GetNode<Marker2D>("Body/Gun/Bullet2");

		LoadSettings();
		Settings.Settings2D.OnSettingsChanged += LoadSettings;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		while (!IsBusy && _targets.Count > 0)
		{
			var (target, callback) = _targets.Pop();
			ShootTarget(target, callback);
		}
	}

	private void LoadSettings()
	{
		ProjectileProps = new WeaponProjectileProps
		{
			PBody = new PBodyProps(
				Settings.Settings2D.DefaultGun.BulletMass,
				0f,
				Settings.Settings2D.DefaultGun.AirResistance,
				0f,
				0f,
				Settings.Settings2D.GravityVector,
				Vector2.Zero
			),
			Speed = Settings.Settings2D.DefaultGun.BulletSpeed
		};

		_rotSpeed = Settings.Settings2D.DefaultGun.AngularVelocity;
	}

	public void OnTargetDetected(BallisticBody target, WeaponCallback callback)
	{
		_targets.Push((target, callback));
	}

	private async void ShootTarget(BallisticBody target, WeaponCallback callback)
	{
		IsBusy = true;

		for (var i = 0; i < Settings.Settings2D.DefaultGun.SalvoSize; ++i)
		{
			if (!IsInstanceValid(target))
				break;

			var result = new CannonSolver(this, target, new SolverOptions()).Aim();
			await LoadAndFire(result.Angle, result.ColTime, callback);

			if (i < Settings.Settings2D.DefaultGun.SalvoSize - 1)
				await Task.Delay(TimeSpan.FromSeconds(60f / Settings.Settings2D.DefaultGun.FireRate));
		}
		
		IsBusy = false;
	}

	private async Task LoadAndFire(float angle, float colTime, WeaponCallback callback)
	{
		angle = 0.5f * Mathf.Pi - angle;
		var duration = Utils.AngleDiff(_gun.Rotation, angle) / _rotSpeed;

		var tween = CreateTween();
		tween.TweenProperty(_gun, "rotation", angle, duration);

		await Utils.AwaitTween(tween);
		await Fire(colTime, callback);
	}

	private async Task Fire(float colTime, WeaponCallback callback)
	{
		var bullets = new[]
		{
			NewProjectile(_bulletPos1.GlobalPosition),
			NewProjectile(_bulletPos2.GlobalPosition)
		};

		var missed = 0;

		foreach (var bullet in bullets)
		{
			bullet.Freeze = true;
			bullet.MaxLifespan = colTime + 1f;

			bullet.Exploded += target =>
			{
				if (IsInstanceValid(target) && !target.HasExploded)
				{
					target.Destroy();
					callback.Invoke(bullets.Length, 0);
				}
				else if (!IsInstanceValid(target) && missed == bullets.Length - 1)
					callback.Invoke(bullets.Length, 0);
				else if (!IsInstanceValid(target))
					++missed;
			};
			
			GetParent().AddChild(bullet);
		}

		await Task.Delay(TimeSpan.FromSeconds(Settings.Settings2D.DefaultGun.FiringDelay));

		foreach (var bullet in bullets)
		{
			if (IsInstanceValid(bullet))
				bullet.Freeze = false;
		}
	}

	public PBodyState NewProjectileState(float angle)
	{
		angle = 0.5f * Mathf.Pi - angle;
		var transform = Transform2D.Identity.Rotated(angle - _gun.GlobalRotation);

		return new PBodyState(
			_gun.GlobalPosition + transform * (_bulletPos1.GlobalPosition - _gun.GlobalPosition),
			ProjectileProps.Speed * (transform * -_gun.GlobalTransform.Y.Normalized()).Normalized()
		);
	}

	private Bullet NewProjectile(Vector2 position)
	{
		var bullet = (_bulletScene.Instantiate() as Bullet)!;

		bullet.Reset(
			ProjectileProps.PBody,
			new PBodyState(position, ProjectileProps.Speed * -_gun.GlobalTransform.Y.Normalized()));

		return bullet;
	}

	public float MeasureDelay(float targetAngle)
	{
		return Utils.AngleDiff(targetAngle, GetAngle()) / _rotSpeed + Settings.Settings2D.DefaultGun.FiringDelay;
	}

	private float GetAngle()
	{
		return 0.5f * Mathf.Pi - _gun.Rotation;
	}
}
