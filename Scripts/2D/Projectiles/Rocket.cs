using System;
using Godot;

namespace Zenitka.Scripts._2D.Projectiles;

public partial class Rocket : BallisticBody
{
	private AnimatedSprite2D _animation;

	private CollisionShape2D _rocketCollision;

	private Marker2D _leftEnginePos;
	private Marker2D _rightEnginePos;

	private float _currentFuel;

	public BallisticBody TrackedTarget { get; set; }

	public override void _Ready()
	{
		base._Ready();

		_animation = (GetChild(0) as AnimatedSprite2D)!;
		_rocketCollision = (GetChild(1) as CollisionShape2D)!;

		_leftEnginePos = GetNode<Marker2D>("LeftEnginePos");
		_rightEnginePos = GetNode<Marker2D>("RightEnginePos");

		_currentFuel = Settings.Settings2D.RocketGun.FuelMass;

		_animation.Play("fly");
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D pState)
	{
		base._IntegrateForces(pState);

		if (!IsInstanceValid(TrackedTarget))
		{
			Destroy();
			return;
		}

		if (SimulationTime > Settings.Settings2D.RocketGun.SideEActivationDelay)
		{
			var d = TrackedTarget.GlobalPosition - pState.Transform.Origin;
			// var result = Solver.Simulate(PBody, TrackedTarget.PBody, 400, 1f / 60f, 0f);
			//
			// pState.Transform = pState.Transform.X.Cross(d) < 0f
			//     ? new Transform2D(pState.Transform.Rotation - 4f * pState.Step, pState.Transform.Origin)
			//     : new Transform2D(pState.Transform.Rotation + 4f * pState.Step, pState.Transform.Origin);

			pState.Transform = new Transform2D(Mathf.LerpAngle(pState.Transform.Rotation, d.Angle(), 2f * pState.Step), pState.Transform.Origin);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!IsInstanceValid(TrackedTarget))
		{
			Destroy();
			return;
		}

		if (_currentFuel > 0 && (TrackedTarget.GlobalPosition - GlobalPosition).Length() > 10f)
			_currentFuel -= Settings.Settings2D.RocketTarget.FuelCost * (float)delta;
		else
		{
			if (HasExploded)
				return;

			Destroy();
		}
	}

	public override void Destroy()
	{
		_animation.Play("explode");

		_rocketCollision.SetDeferred("disabled", true);

		if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
			_animation.AnimationLooped += QueueFree;

		HasExploded = true;
	}

	private void _on_body_entered(Node body)
	{
		if (body is BallisticBody target)
			target.Destroy();

		Destroy();
	}

	private void _on_button_pressed()
	{
		Destroy();
	}
}
