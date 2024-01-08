using System;
using Godot;

namespace Zenitka.Scripts._2D.Projectiles;

public partial class Bullet : BallisticBody
{
	private AnimatedSprite2D _animation;

	private Timer _timer;
	private float _maxLifespan;

	private CollisionShape2D _bulletCollision;
	private CollisionShape2D _explosionCollision;

	public float MaxLifespan
	{
		set
		{
			_maxLifespan = value;
			
			if (_maxLifespan > 0 && IsInstanceValid(_timer))
				_timer.Start(_maxLifespan);
		}
		get => _maxLifespan;
	}

	public override void _Ready()
	{
		base._Ready();
		
		_animation = (GetChild(0) as AnimatedSprite2D)!;
		_timer = (GetNode("SelfDestructionTimer") as Timer)!;
		_bulletCollision = (GetChild(1) as CollisionShape2D)!;
		_explosionCollision = (GetChild(3) as CollisionShape2D)!;

		_timer.WaitTime = MaxLifespan;
		_timer.Start();

		_animation.Play("fly2");
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D pState)
	{
		base._IntegrateForces(pState);
		
		if (pState.LinearVelocity != Vector2.Zero)
			pState.Transform = new Transform2D(pState.LinearVelocity.Angle(), pState.Transform.Origin);
	}

	private void OnBodyEntered(Node body)
	{
		if (body is IWeapon)
			body.QueueFree();
		
		if (body is BallisticBody target)
		{
			OnExploded(target);
			target.Destroy();
		}
		else
			OnExploded(null);
			
		Explode();
	}

	private void SelfDestroy()
	{
		Explode();
	}

	private void Explode()
	{
		_animation.Play("explode");

		if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
			_animation.AnimationLooped += QueueFree;

		_bulletCollision.SetDeferred("disabled", true);
		_explosionCollision.SetDeferred("disabled", false);

		HasExploded = true;
	}
}
