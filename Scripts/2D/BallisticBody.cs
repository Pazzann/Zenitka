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
			force += Velocity.Normalized() * props.MainEThrust - (props.QDrag * 0.0002f) * Velocity * Velocity.Length();	
		
		return force / props.Mass + props.ConstantAcceleration;
	}
}

public partial class BallisticBody : RigidBody2D
{
	private static readonly Random Rng = new();

	private float _simulationTime;

	public PBodyProps Props { get; private set; }
	public PBodyState State { get; private set; }

	protected bool HasExploded;
	private bool _firstFrameAfterExplosion = true;

	public override void _Ready()
	{
		Reset();
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D physicsState)
	{
		base._IntegrateForces(physicsState);

		physicsState.LinearVelocity = State.Velocity;
		Rotation = State.Velocity.Angle();
	}

	public override void _PhysicsProcess(double delta)
	{
		var deltaF = (float)delta;
		_simulationTime += deltaF;

		State = ApplyRandomness(State.Update(deltaF, Props));

		if (HasExploded && _firstFrameAfterExplosion)
		{
			State = State with { Velocity = State.Velocity / 5f };
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
		GravityScale = 0f;
		_simulationTime = 0f;
	}

	private PBodyState ApplyRandomness(PBodyState state)
	{
		var kindX = Rng.Next(2) == 1;
		var randFactorX = Rng.Next(Settings.Settings2D.Random);
		var randEnvX = kindX ? 1 - randFactorX * 0.001f : 1 + randFactorX * 0.001f;

		var kindY = Rng.Next(2) == 1;
		var randFactorY = Rng.Next(Settings.Settings2D.Random);
		var randEnvY = kindY ? 1 - randFactorY * 0.001f : 1 + randFactorY * 0.001f;
		
		return state with { Velocity = state.Velocity * new Vector2(randEnvX, randEnvY) };
	}
}
