using Godot;
using static Zenitka.Scripts.MathUtils;

namespace Zenitka.Scripts._3D
{
	public struct BodyState3D
	{
		public Vector3 Position, Velocity, ConstantAcceleration;
		public float LinearDrag, SelfPropellingAcceleration, Mass;

		public BodyState3D()
		{
			Position = Vector3.Zero;
			Velocity = Vector3.Zero;
			ConstantAcceleration = Vector3.Zero;
			LinearDrag = 0f;
			SelfPropellingAcceleration = 0f;
			Mass = 1f;
		}

		public BodyState3D(Vector3 position,
						   Vector3 velocity,
						   Vector3 constantAcceleration,
						   float linearDrag,
						   float selfPropellingAcceleration,
						   float mass) 
		{
			Position = position;
			Velocity = velocity;
			ConstantAcceleration = constantAcceleration;
			LinearDrag = linearDrag;
			SelfPropellingAcceleration = selfPropellingAcceleration;
			Mass = mass;
		}

		public readonly Vector3 ComputeAcceleration(in Vector3 curVelocity)
		{
			var a = ConstantAcceleration - LinearDrag / Mass * curVelocity;

			if (curVelocity != Vector3.Zero)
				a += curVelocity.Normalized() * SelfPropellingAcceleration;

			return a;
		}

		public readonly Vector3 ComputeAccelerationDerivative(in Vector3 curVelocity, in Vector3 acceleration)
		{
			var a = -LinearDrag / Mass * Vector3.One;

			if (curVelocity != Vector3.Zero)
				a += curVelocity.Normalized() * SelfPropellingAcceleration;

			return a;
		}

		public readonly void Integrate(ref Vector3 curPosition, ref Vector3 curVelocity, float dt)
		{
			var acceleration = ComputeAcceleration(curVelocity);

			curVelocity += acceleration * dt;
			curPosition += curVelocity * dt;
		}
	}

	public readonly struct ProjectileConfig3D {
		public readonly float Speed, Acceleration, SelfPropellingAcceleration, LinearDrag, Mass;

		public ProjectileConfig3D(float velocity,
								  float acceleration, 
								  float selfPropellingAcceleration, 
								  float linearDrag, 
								  float mass) 
		{
			Speed = velocity;
			Acceleration = acceleration;
			SelfPropellingAcceleration = selfPropellingAcceleration;
			LinearDrag = linearDrag;
			Mass = mass;
		}
	}

	public struct CannonState3D
	{
		public Vector3 VOrigin, HOrigin, BulletPos;
		public float HRotSpeed, VRotSpeed, BarrelLength, HAngle, VAngle;
		public ProjectileConfig3D Projectile;

		public CannonState3D(
			Vector3 vOrigin,
			Vector3 hOrigin,
			Vector3 bulletPos,
			float hAngle,
			float vAngle,
			float length,
			float hRotSpeed,
			float vRotSpeed,
			ProjectileConfig3D projectile)
		{
			VOrigin = vOrigin;
			HOrigin = hOrigin;
			BulletPos = bulletPos;
			HAngle = hAngle;
			VAngle = vAngle;
			BarrelLength = length;
			HRotSpeed = hRotSpeed;
			VRotSpeed = vRotSpeed;
			Projectile = projectile;
		}

		public readonly BodyState3D CreateProjectile(float hAngle, float vAngle, in Vector3 gravity)
		{
			var transform = Rotated(HOrigin, Vector3.Up, -hAngle) * Rotated(VOrigin, Vector3.Back, vAngle);

			return new BodyState3D(
				transform * BulletPos,
				transform * (Projectile.Speed * Vector3.Right),
				transform * (Projectile.Acceleration * Vector3.Right) + gravity,
				Projectile.LinearDrag,
				Projectile.SelfPropellingAcceleration,
				Projectile.Mass);
		}
	}

	public class Solver3D
	{
		private const int SIMULATION_STEPS = 400;
		private const float SIMULATION_TIME_STEP = 1f / 60f;
		private const float INF = 9999f;

		private CannonState3D _cannon;
		private BodyState3D _target;
		private Vector3 _gravity;

		public Solver3D(CannonState3D cannon, BodyState3D target, Vector3 gravity)
		{
			_cannon = cannon;
			_target = target;
			_gravity = gravity;
		}

		// (hAngle, vAngle, timeOfCollision, projectile)
		public (float, float, float, BodyState3D) Aim2(float eps = 1e-4F) {
			var simulationOptions = new SimulationOptions(SIMULATION_STEPS, SIMULATION_TIME_STEP);
			var (hAngle, vAngle, bestResult) = AimH(simulationOptions, eps);

			return (hAngle, vAngle, bestResult.ClosestDistanceTime, bestResult.Projectile);
		}

		private (float, float, SimulationResult) AimH(SimulationOptions options, float eps, float maxHIter = 10, float maxVIter = 10) {
			var bestResult = new SimulationResult(_cannon.CreateProjectile(_cannon.HAngle, _cannon.VAngle, _gravity));
			float bestVAngle = _cannon.VAngle, bestHAngle = _cannon.HAngle;

			var d = ProjectOnXZ(_target.Position) - ProjectOnXZ(_cannon.HOrigin);

			var baseDir = d - d.Project(ProjectOnXZ(_target.Velocity));
			var baseHAngle = Mathf.PosMod(baseDir.Angle(), 2f * Mathf.Pi);

			float minHAngle = baseHAngle, maxHAngle = baseHAngle + Mathf.Pi / 2f;

			for (int i = 0; i < maxHIter && maxHAngle - minHAngle > eps; ++i) {
				float hAngle1 = Mathf.Lerp(minHAngle, maxHAngle, 1f / 3f);
				var (vAngle1, result1) = AimV(hAngle1, options, eps, maxVIter);

				float hAngle2 = Mathf.Lerp(minHAngle, maxHAngle, 2f / 3f);
				var (vAngle2, result2) = AimV(hAngle2, options, eps, maxVIter);

				if (result1.ClosestDistance < result2.ClosestDistance)
					maxHAngle = hAngle2;
				else
					minHAngle = hAngle1;

				if (result1.ClosestDistance < bestResult.ClosestDistance) {
					bestResult = result1;
					bestVAngle = vAngle1;
					bestHAngle = hAngle1;
				}

				if (result2.ClosestDistance < bestResult.ClosestDistance) {
					bestResult = result2;
					bestVAngle = vAngle2;
					bestHAngle = hAngle2;
				}
			}

			minHAngle = baseHAngle - Mathf.Pi / 2f;
			maxHAngle = baseHAngle;

			for (int i = 0; i < maxHIter && maxHAngle - minHAngle > eps; ++i) {
				float hAngle1 = Mathf.Lerp(minHAngle, maxHAngle, 1f / 3f);
				var (vAngle1, result1) = AimV(hAngle1, options, eps, maxVIter);

				float hAngle2 = Mathf.Lerp(minHAngle, maxHAngle, 2f / 3f);
				var (vAngle2, result2) = AimV(hAngle2, options, eps, maxVIter);

				if (result1.ClosestDistance < result2.ClosestDistance)
					maxHAngle = hAngle2;
				else
					minHAngle = hAngle1;

				if (result1.ClosestDistance < bestResult.ClosestDistance) {
					bestResult = result1;
					bestVAngle = vAngle1;
					bestHAngle = hAngle1;
				}

				if (result2.ClosestDistance < bestResult.ClosestDistance) {
					bestResult = result2;
					bestVAngle = vAngle2;
					bestHAngle = hAngle2;
				}
			}

			GD.Print(bestResult.ClosestDistance);
			return (bestHAngle, bestVAngle, bestResult);
		}

		private (float, SimulationResult) AimV(float hAngle, SimulationOptions options, float eps, float maxIter) {
			var bestResult = new SimulationResult(_cannon.CreateProjectile(_cannon.HAngle, _cannon.VAngle, _gravity));
			var bestVAngle = 0f;

			float minVAngle = 0f, maxVAngle = Mathf.Pi / 2f; 

			for (int i = 0; i < maxIter && maxVAngle - minVAngle > eps; ++i) {
				float vAngle1 = Mathf.Lerp(minVAngle, maxVAngle, 1f / 3f);
				var result1 = Simulate(hAngle, vAngle1, options);

				float vAngle2 = Mathf.Lerp(minVAngle, maxVAngle, 2f / 3f);
				var result2 = Simulate(hAngle, vAngle2, options);

				if (result1.ClosestDistance < result2.ClosestDistance)
					maxVAngle = vAngle2;
				else
					minVAngle = vAngle1;

				if (result1.ClosestDistance < bestResult.ClosestDistance) {
					bestResult = result1;
					bestVAngle = vAngle1;
				}

				if (result2.ClosestDistance < bestResult.ClosestDistance) {
					bestResult = result2;
					bestVAngle = vAngle2;
				}
			}

			return (bestVAngle, bestResult);
		}

		private readonly struct SimulationOptions {
			public readonly int Steps;
			public readonly float TimeStep;

			public SimulationOptions(int steps, float timeStep) {
				Steps = steps;
				TimeStep = timeStep;
			}
		}

		private struct SimulationResult
		{
			public float ClosestDistance = INF;
			public float ClosestDistanceTime = 0;
			public Vector3 ClosestDistanceDeltaPos = Vector3.Zero;
			public BodyState3D Projectile = new BodyState3D();

			public SimulationResult(BodyState3D projectile) {
				Projectile = projectile;
			}
		}

		private SimulationResult Simulate(float hAngle, float vAngle, in SimulationOptions options)
		{
			var projectile = _cannon.CreateProjectile(hAngle, vAngle, _gravity);
			var result = new SimulationResult(projectile);

			var offT = AngleDiff(vAngle, _cannon.VAngle) / _cannon.VRotSpeed + AngleDiff(hAngle, _cannon.HAngle) / _cannon.HRotSpeed;

			Vector3 p1 = projectile.Position, p2 = _target.Position, v1 = projectile.Velocity, v2 = _target.Velocity;

			for (int i = 0; i < options.Steps; ++i)
			{
				var t = options.TimeStep * i;

				if (t >= offT)
					projectile.Integrate(ref p1, ref v1, options.TimeStep);

				_target.Integrate(ref p2, ref v2, options.TimeStep);

				var distance = (p1 - p2).Length();

				if (distance < result.ClosestDistance)
				{
					result.ClosestDistanceTime = t;
					result.ClosestDistance = distance;
					result.ClosestDistanceDeltaPos = p1 - p2;
				}
			}

			result.Projectile = projectile;
			return result;
		}
	}
}
