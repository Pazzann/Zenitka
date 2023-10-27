using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using Godot;

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

        public readonly ParticleState2D CreateProjectile(float angle, Vector2 gravity)
        {
            var d = Vector2.FromAngle(angle);
            d.Y = -d.Y;

            return new ParticleState2D(
                Position + BarrelLength * d,
                ProjectileSpeed * d,
                gravity + d * ProjectileAcceleration,
                ProjectileLinearDrag,
                ProjectileSelfPropellingAcceleration,
                ProjectileMass);
        }
    }

    public struct ParticleState2D
    {
        public Vector2 Position, Velocity, ConstantAcceleration;
        public float LinearDrag, SelfPropellingAcceleration, Mass;

        public ParticleState2D(Vector2 position,
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
        private const int SECTORS = 600;
        private const int ITERATIONS = 600;

        private const float SECTOR_ARC = Mathf.Pi / SECTORS;
        private const float TIME_STEP = 1f / 60f;

        private CannonState2D _cannon;
        private ParticleState2D _target;
        private Vector2 _gravity;

        public Solver2D(CannonState2D cannon, ParticleState2D target, Vector2 gravity)
        {
            _cannon = cannon;
            _target = target;
            _gravity = gravity;
        }

        public (float, float) Aim()
        {
            var bestAngle = 0f;
            var bestTimeOfCollision = 9999f;
            var bestDistance = float.MaxValue;

            for (int i = 0; i < SECTORS; ++i)
            {
                float angle = i * SECTOR_ARC + SECTOR_ARC / 2f;
                var (distance, time) = ComputeClosestDistance(angle);

                if (distance < bestDistance)
                {
                    bestAngle = angle;
                    bestTimeOfCollision = time;
                    bestDistance = distance;
                }
            }

            //GD.Print("Best angle: ", bestAngle);
            //GD.Print("Best time: ", bestTimeOfCollision);
            GD.Print("Best distance: ", bestDistance);

            return (bestAngle, bestTimeOfCollision);
        }

        // (distance, time)
        private (float, float) ComputeClosestDistance(float angle, bool useNumericalIntegration = true)
        {
            var projectile = _cannon.CreateProjectile(angle, _gravity);

            var bestT = 0f;
            var bestDistance = 9999f;

            var offT = Mathf.Abs(angle - _cannon.CurAngle) / _cannon.AngularRotSpeed;

            Vector2 p1 = projectile.Position, p2 = _target.Position, v1 = projectile.Velocity, v2 = _target.Velocity;

            for (int i = 0; i < ITERATIONS; ++i)
            {
                var t = TIME_STEP * i;

                if (useNumericalIntegration)
                {
                    if (t >= offT)
                        projectile.Integrate(ref p1, ref v1, TIME_STEP);

                    _target.Integrate(ref p2, ref v2, TIME_STEP);
                }
                else
                {
                    p1 = projectile.ComputePosition(t);
                    p2 = _target.ComputePosition(t + offT);
                }

                var distance = (p1 - p2).Length();

                if (distance < bestDistance)
                {
                    bestT = t;
                    bestDistance = distance;
                }
            }

            return (bestDistance, bestT);
        }
    }
}