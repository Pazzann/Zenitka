using Godot;

namespace Zenitka.Scripts._2D.Projectiles;

public partial class Target : BallisticBody
{
	private AnimatedSprite2D _animation;
	private CollisionShape2D _rocketCollision;

	public override void _Ready()
	{
		_animation = (GetChild(1) as AnimatedSprite2D)!;
		_rocketCollision = (GetChild(0) as CollisionShape2D)!;
			
		_animation.Play("fly");

		base._Ready();
	}
	
	public override void _IntegrateForces(PhysicsDirectBodyState2D pState)
	{
		base._IntegrateForces(pState);
		
		if (pState.LinearVelocity != Vector2.Zero)
			pState.Transform = new Transform2D(pState.LinearVelocity.Angle(), pState.Transform.Origin);
	}

	public override void Destroy()
	{
		if (HasExploded)
			return;
			
		_rocketCollision.SetDeferred("disabled", true);

		_animation.Play("explode");

		if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
			_animation.AnimationLooped += QueueFree;

		HasExploded = true;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is StaticBody2D)
			Destroy();
	}
}
