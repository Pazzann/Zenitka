using Godot;

namespace Zenitka.Scripts._2D.Projectiles;

public partial class RocketTarget : BallisticBody
{
	private AnimatedSprite2D _animation;
	private CollisionShape2D _rocketCollision;
	
	private float _currentFuel;

	public override void _Ready()
	{
		_animation = (GetChild(0) as AnimatedSprite2D)!;
		_rocketCollision = (GetChild(1) as CollisionShape2D)!;

		_currentFuel = Settings.Settings2D.RocketTarget.FuelMass;

		_animation.Play("fly");

		base._Ready();
	}
	
	public override void _IntegrateForces(PhysicsDirectBodyState2D pState)
	{
		base._IntegrateForces(pState);
		
		if (pState.LinearVelocity != Vector2.Zero)
			pState.Transform = new Transform2D(pState.LinearVelocity.Angle(), pState.Transform.Origin);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (_currentFuel > 0)
		{
			_currentFuel -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
			Mass -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
		}
		else
		{
			if (HasExploded)
				return;
				
			_currentFuel = 0;
			Destroy();
		}
	}

	private void _on_button_pressed()
	{
		Destroy();
	}
	
	private void OnBodyEntered(Node body)
	{
		if (body is IWeapon)
			body.QueueFree();
		
		if (body is StaticBody2D)
			Destroy();
		
		if (body is BallisticBody)
		{
			OnExploded(body as BallisticBody);
			Destroy();
		}
	}

	public override void Destroy()
	{
		HasExploded = true;
		
		_animation.Play("explode");

		if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
			_animation.AnimationLooped += QueueFree;

		_rocketCollision.SetDeferred("disabled", true);
	}
}
