using Godot;
using Zenitka.Scripts._2D.Targets;

namespace Zenitka.Scripts._2D;

public partial class Cannon : Node2D, IWeapon
{
	private PackedScene _bulletScene;

	private Sprite2D _gun;
	private Node2D _bulletPos1;
	private Node2D _bulletPos2;

	private float _rotSpeed;
	public WeaponProjectileProps ProjectileProps { get; private set; }

	public override void _Ready()
	{
		_bulletScene = GD.Load<PackedScene>("res://Prefabs/2D/Bullet.tscn");

		_gun = GetNode<Sprite2D>("Gun");
		_bulletPos1 = GetNode<Node2D>("Gun/Bullet1");
		_bulletPos2 = GetNode<Node2D>("Gun/Bullet2");

		OnSettingsChanged();
		Settings.Settings2D.OnSettingsChanged += OnSettingsChanged;
	}

	private void OnSettingsChanged()
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
		var result = new Solver(this, target, new SolverOptions()).Aim();
		LoadAndFire(result.Angle, result.ColTime, callback);
	}

	private void LoadAndFire(float angle, float colTime, WeaponCallback callback)
	{
		angle = 0.5f * Mathf.Pi - angle;

		var targetTransform = Transform2D.Identity.Rotated(angle);
		var duration = Utils.AngleDiff(_gun.Rotation, angle) / _rotSpeed;

		var tween = CreateTween();
		tween.TweenProperty(_gun, "transform", targetTransform, duration);

		tween.TweenCallback(Callable.From(() => Fire(colTime, callback)));
	}

	private void Fire(float colTime, WeaponCallback callback)
	{
		for (var i = 0; i < 1; ++i)
		{
			var bullets = new[]
			{
				NewProjectile(_bulletPos1.GlobalPosition),
				NewProjectile(_bulletPos2.GlobalPosition)
			};

			var missed = 0;
			var hit = false;

			foreach (var bullet in bullets)
			{
				bullet.MaxLifespan = colTime + 1f;

				bullet.OnExploded += target =>
				{
					if (target != null && !hit)
					{
						hit = true;
						callback.Invoke(bullets.Length, 1);
					}
					else if (target == null && missed == bullets.Length - 1)
						callback.Invoke(bullets.Length, 0);
					else if (target == null)
						++missed;
				};

				AddChild(bullet);
			}
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

	public float MeasureRotationTime(float targetAngle)
	{
		return Utils.AngleDiff(targetAngle, GetAngle()) / _rotSpeed;
	}

	private float GetAngle()
	{
		return 0.5f * Mathf.Pi - _gun.Rotation;
	}
}
