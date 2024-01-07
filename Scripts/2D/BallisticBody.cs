using Godot;
using System;

namespace Zenitka.Scripts._2D;

public record PBodyProps(
	float Mass = 0f,
	float LDrag = 0f,
	float QDrag = 0f,
	float MainEThrust = 0f,
	float SideEThrust = 0f,
	Vector2 ConstantAcceleration = new Vector2(),
	Vector2 Extents = new Vector2())
{
	public PBodyProps Copied()
	{
		return this with { };
	}
}

public readonly record struct PBodyState(Vector2 Position, Vector2 Velocity)
{
	public PBodyState Update(float dt, in PBodyProps props)
	{
		var acceleration = GetAcceleration(props);
		var velocity = Velocity + acceleration * dt;
		var position = Position + velocity * dt;

		return new PBodyState(position, velocity);
	}

	private Vector2 GetAcceleration(in PBodyProps props)
	{
		var force = -props.LDrag * Velocity;

		if (Velocity != Vector2.Zero)
			force += Velocity.Normalized() * props.MainEThrust - props.QDrag * 0.0002f * Velocity * Velocity.Length();

		return force / props.Mass + props.ConstantAcceleration;
	}
}

public partial class BallisticBody : RigidBody2D
{
	private static readonly Random Rng = new();

	public PBodyProps Props { get; private set; }
	public PBodyState State { get; private set; }

	protected float SimulationTime { get; private set; }

	protected bool HasExploded;
	private bool _firstFrameAfterExplosion = true;

	public override void _Ready()
	{
		Reset();
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D pState)
	{
		SimulationTime += pState.Step;

		ApplyForce(Props.MainEThrust * pState.Transform.X.Normalized() -
				   Props.QDrag * 0.0002f * pState.LinearVelocity * pState.LinearVelocity.Length() -
				   Props.LDrag * pState.LinearVelocity);

		pState.LinearVelocity = ApplyRandomness(pState.LinearVelocity);

		if (pState.LinearVelocity != Vector2.Zero)
			pState.Transform = new Transform2D(pState.LinearVelocity.Angle(), pState.Transform.Origin);

		State = new PBodyState(pState.Transform.Origin, pState.LinearVelocity);

		if (HasExploded && _firstFrameAfterExplosion)
		{
			pState.LinearVelocity /= 5f;
			_firstFrameAfterExplosion = false;
		}
	}

	public void Reset(PBodyProps props, PBodyState state)
	{
		Props = props;
		State = state;

		if (GetParent() != null)
			Reset();
	}

	public virtual void Destroy()
	{
		QueueFree();
	}

	private void Reset()
	{
		GlobalPosition = State.Position;
		LinearVelocity = State.Velocity;
		AngularVelocity = 0f;
		Mass = Props.Mass;
		Inertia = 0f;
		ConstantForce = Props.Mass * Props.ConstantAcceleration;
		Rotation = State.Velocity.Angle();
		SimulationTime = 0f;
	}

	private Vector2 ApplyRandomness(Vector2 v)
	{
		var kindX = Rng.Next(2) == 1;
		var randFactorX = Rng.Next(Settings.Settings2D.Random);
		var randEnvX = kindX ? 1 - randFactorX * 0.001f : 1 + randFactorX * 0.001f;

		var kindY = Rng.Next(2) == 1;
		var randFactorY = Rng.Next(Settings.Settings2D.Random);
		var randEnvY = kindY ? 1 - randFactorY * 0.001f : 1 + randFactorY * 0.001f;

		return v * new Vector2(randEnvX, randEnvY);
	}
}
