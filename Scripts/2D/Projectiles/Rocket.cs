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
			pState.LinearVelocity = pState.LinearVelocity.Length() * pState.Transform.X.Normalized();
			
			// var front = pState.Transform.X.Normalized();
			//
			// var r1 = _rightEnginePos.GlobalPosition - pState.Transform.Origin;
			// var r2 = _leftEnginePos.GlobalPosition - pState.Transform.Origin;
			//
			// var torque1 = r1.Cross(front * Props.SideEThrust);
			// var torque2 = r2.Cross(front * Props.SideEThrust);
			//
			// var angularVelocity1 = pState.AngularVelocity + torque1 * pState.Step;
			// var angularVelocity2 = pState.AngularVelocity + torque2 * pState.Step;

			float step = Props.SideEThrust / 8000f * pState.Step;

			var state1 = new PBodyState(pState.Transform.Origin, pState.LinearVelocity.Rotated(step));
			var result1 = Solver.Simulate(new PBody(Props, state1), TrackedTarget.PBody, 20, 0.1f, 0f);
			
			var state2 = new PBodyState(pState.Transform.Origin, pState.LinearVelocity.Rotated(-step));
			var result2 = Solver.Simulate(new PBody(Props, state2), TrackedTarget.PBody, 20, 0.1f, 0f);

			//var result3 = Solver.Simulate(PBody, TrackedTarget.PBody, 10, 0.2f, 0f);

			// if (result3.AbsError < Mathf.Min(result1.AbsError, result2.AbsError))
			// 	return;

			if (result1.AbsError < result2.AbsError)
			{
				pState.Transform = pState.Transform.RotatedLocal(step);
			}
			else
			{
				pState.Transform = pState.Transform.RotatedLocal(-step);
			}

			// if (result1.AbsError < result2.AbsError)
			// 	pState.LinearVelocity = pState.LinearVelocity.Rotated(pState.Step);
			// else
			// 	pState.LinearVelocity = pState.LinearVelocity.Rotated(-pState.Step);

			// if (result1.AbsError < result2.AbsError)
			// 	pState.ApplyForce(front * Props.SideEThrust, r2);
			// else
			// 	pState.ApplyForce(front * Props.SideEThrust, r1);
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
		LinearVelocity = Vector2.Zero;
		_animation.Play("explode");

		_rocketCollision.SetDeferred("disabled", true);

		if (!_animation.IsConnected(AnimatedSprite2D.SignalName.AnimationLooped, Callable.From(QueueFree)))
			_animation.AnimationLooped += QueueFree;

		HasExploded = true;
	}

	private void _on_body_entered(Node body)
	{
		if (body is IWeapon)
			body.QueueFree();

		if (body is BallisticBody target)
		{
			OnExploded(target);
			target.Destroy();
		}

		Destroy();
	}

	private void _on_button_pressed()
	{
		Destroy();
	}
}
