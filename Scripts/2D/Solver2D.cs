using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Xml.Linq;
using Godot;
using Microsoft.VisualBasic;

namespace Zenitka.Scripts._2D
{
    public struct CannonState2D
    {
        public Vector2 Position;
        public float BarrelLength, CurAngle, AngularRotSpeed, ProjectileSpeed, ProjectileAcceleration, ProjectileLinearDrag, ProjectileMass;
        public float ProjectileSelfPropellingAcceleration;

        public CannonState2D(Vector2 position,
                             float barrelLength,
                             float curAngle,
                             float angularRotSpeed,
                             float projectileSpeed,
                             float projectileAcceleration,
                             float projectileLinearDrag,
                             float projectileSelfPropellingAcceleration,
                             float projectileMass)
        {
            Position = position;
            BarrelLength = barrelLength;
            CurAngle = curAngle;
            AngularRotSpeed = angularRotSpeed;
            ProjectileSpeed = projectileSpeed;
            ProjectileAcceleration = projectileAcceleration;
            ProjectileLinearDrag = projectileLinearDrag;
            ProjectileSelfPropellingAcceleration = projectileSelfPropellingAcceleration;
            ProjectileMass = projectileMass;
        }

        public readonly BodyState2D CreateProjectile(float angle, Vector2 gravity)
        {
            var d = Vector2.FromAngle(angle);
            d.Y = -d.Y;

            return new BodyState2D(
                Position + BarrelLength * d,
                ProjectileSpeed * d,
                gravity + d * ProjectileAcceleration,
                ProjectileLinearDrag,
                ProjectileSelfPropellingAcceleration,
                ProjectileMass);
        }
    }

    public struct BodyState2D
    {
        public Vector2 Position, Velocity, ConstantAcceleration;
        public float LinearDrag, SelfPropellingAcceleration, Mass;

        public BodyState2D(Vector2 position,
                           Vector2 velocity,
                           Vector2 constantAcceleration,
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

        public readonly Vector2 ComputePosition(float t)
        {
            if (LinearDrag == 0f)
                return ConstantAcceleration * t * t / 2f + Velocity * t;

            float dragCoef = LinearDrag / Mass;

            return Position
                + ConstantAcceleration * t / dragCoef
                - (ConstantAcceleration - dragCoef * Velocity) / (dragCoef * dragCoef)
                + (ConstantAcceleration - dragCoef * Velocity) * Mathf.Exp(-dragCoef * t) / (dragCoef * dragCoef);
        }

        public readonly Vector2 ComputeVelocity(float t)
        {
            if (LinearDrag == 0f)
                return ConstantAcceleration * t + Velocity;

            float dragCoef = LinearDrag / Mass;

            return (ConstantAcceleration
                - (ConstantAcceleration - dragCoef * Velocity) * Mathf.Exp(-dragCoef * t)) / dragCoef;
        }

        public readonly Vector2 ComputeAcceleration(in Vector2 curVelocity)
        {
            var a = ConstantAcceleration - LinearDrag / Mass * curVelocity;

            if (curVelocity != Vector2.Zero)
                a += curVelocity.Normalized() * SelfPropellingAcceleration;

            return a;
        }

        public readonly Vector2 ComputeAccelerationDerivative(in Vector2 curVelocity, in Vector2 acceleration)
        {
            var a = -LinearDrag / Mass * Vector2.One;

            if (curVelocity != Vector2.Zero)
                a += curVelocity.Normalized() * SelfPropellingAcceleration;

            return a;
        }

        public readonly void Integrate(ref Vector2 curPosition, ref Vector2 curVelocity, float dt)
        {
            var acceleration = ComputeAcceleration(curVelocity);

            curVelocity += acceleration * dt;
            curPosition += curVelocity * dt;
        }
    }

    public class Solver2D
    {
        private const float INF = 9999f;

        private const int SIMULATION_STEPS = 400;
        private const float SIMULATION_TIME_STEP = 1f / 60f;

        private CannonState2D _cannon;
        private BodyState2D _target;
        private Vector2 _gravity;

        public Solver2D(CannonState2D cannon, BodyState2D target, Vector2 gravity)
        {
            _cannon = cannon;
            _target = target;
            _gravity = gravity;
        }

        public (float, float) Aim(float eps = 1e-4F) {
            var (angle1, result1) = Aim(0.1f, Mathf.Pi / 2f, false, eps);
            var (angle2, result2) = Aim(Mathf.Pi / 2f, Mathf.Pi - 0.1f, true, eps);

            if (result1.ClosestDistance < result2.ClosestDistance)
                return (angle1, result1.ClosestDistanceTime);
            else
                return (angle2, result2.ClosestDistanceTime);
        }

        private (float, SimulationResult) Aim(float minAngle, float maxAngle, bool mirrored, float eps)
        {
            const int MAX_ITER = 100;

            var simulationOptions = new SimulationOptions(SIMULATION_STEPS, SIMULATION_TIME_STEP);

            var angle = 0f;
            var result = new SimulationResult();

            for (int i = 0; i < MAX_ITER && maxAngle - minAngle > eps; ++i)
            {
                angle = (minAngle + maxAngle) / 2f;
                result = Simulate(angle, simulationOptions);

                if ((result.ClosestDistanceDeltaPos.Y < 0) ^ mirrored)
                    maxAngle = angle;
                else if ((result.ClosestDistanceDeltaPos.Y > 0) ^ mirrored)
                    minAngle = angle;
            }

            return (angle, result);
        }

        private struct SimulationResult
        {
            public float ClosestDistance = INF;
            public float ClosestDistanceTime = 0;
            public Vector2 ClosestDistanceDeltaPos = Vector2.Zero;

            public SimulationResult()
            {}
        }

        private readonly struct SimulationOptions {
            public readonly bool UseNumericalIntegration;
            public readonly int Steps;
            public readonly float TimeStep;

            public SimulationOptions(int steps, float timeStep) {
                UseNumericalIntegration = false;
                Steps = steps;
                TimeStep = timeStep;
            }
        }

        private SimulationResult Simulate(float angle, in SimulationOptions options)
        {
            var projectile = _cannon.CreateProjectile(angle, _gravity);
            var result = new SimulationResult();

            var offT = Mathf.Abs(angle - _cannon.CurAngle) / _cannon.AngularRotSpeed;

            Vector2 p1 = projectile.Position, p2 = _target.Position, v1 = projectile.Velocity, v2 = _target.Velocity;

            for (int i = 0; i < options.Steps; ++i)
            {
                var t = options.TimeStep * i;

                if (options.UseNumericalIntegration)
                {
                    if (t >= offT)
                        projectile.Integrate(ref p1, ref v1, options.TimeStep);

                    _target.Integrate(ref p2, ref v2, options.TimeStep);
                }
                else
                {
                    p1 = projectile.ComputePosition(t);
                    p2 = _target.ComputePosition(t + offT);
                }

                var distance = (p1 - p2).Length();

                if (distance < result.ClosestDistance)
                {
                    result.ClosestDistanceTime = t;
                    result.ClosestDistance = distance;
                    result.ClosestDistanceDeltaPos = p1 - p2;
                }
            }

            return result;
        }
    }
}