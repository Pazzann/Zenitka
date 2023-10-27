using System.Reflection.Metadata;
using System.Threading.Tasks;
using Godot;
using Microsoft.VisualBasic;

namespace Zenitka.Scripts._3D
{
    public struct ParticleState3D
    {
        public ParticleState3D()
        {
            Position = Vector3.Zero;
            Velocity = Vector3.Zero;
            ConstantAcceleration = Vector3.Zero;
            LinearDrag = 0f;
        }

        public ParticleState3D(Vector3 position, Vector3 velocity, Vector3 constantAcceleration, float linearDrag)
        {
            Position = position;
            Velocity = velocity;
            ConstantAcceleration = constantAcceleration;
            LinearDrag = linearDrag;
        }

        public Vector3 ComputePosition(float t)
        {
            // TODO: consider the case when [`LinearDrag`] equals 0

            return Position
                + ConstantAcceleration * t / LinearDrag
                - (ConstantAcceleration - LinearDrag * Velocity) / (LinearDrag * LinearDrag)
                + (ConstantAcceleration - LinearDrag * Velocity) * Mathf.Exp(-LinearDrag * t) / (LinearDrag * LinearDrag);
        }

        public Vector3 ComputeVelocity(float t)
        {
            // TODO: consider the case when [`LinearDrag`] equals 0

            return (ConstantAcceleration
                - (ConstantAcceleration - LinearDrag * Velocity) * Mathf.Exp(-LinearDrag * t)) / LinearDrag;
        }

        public Vector3 Position, Velocity, ConstantAcceleration;
        public float LinearDrag;
    }

    public struct CannonState3D
    {
        public CannonState3D(
            Vector3 position,
            Vector3 curDirection,
            float barrelLength,
            float angularRotSpeed,
            float projectileSpeed,
            float projectileAcceleration,
            float projectileLinearDrag)
        {
            Position = position;
            CurDirection = curDirection.Normalized();
            AngularRotSpeed = angularRotSpeed;
            ProjectileSpeed = projectileSpeed;
            ProjectileAcceleration = projectileAcceleration;
            ProjectileLinearDrag = projectileLinearDrag;
            BarrelLength = barrelLength;
        }

        // direction vector must be normalized
        public ParticleState3D CreateProjectile(in Vector3 direction, in Vector3 gravity)
        {
            return new ParticleState3D(
                Position + direction * BarrelLength,
                ProjectileSpeed * direction,
                ProjectileAcceleration * direction + gravity,
                ProjectileLinearDrag);
        }

        public Vector3 CurDirection, Position;
        public float AngularRotSpeed, ProjectileSpeed, ProjectileAcceleration, ProjectileLinearDrag, BarrelLength;
    }

    public class Solver3D
    {
        private const int ANGLES = 40;
        private const int ITERATIONS = 50;

        private const float DELTA_ANGLE = Mathf.Pi / ANGLES;
        private const float DELTA_TIME = 0.05f;

        public Solver3D(CannonState3D cannon, ParticleState3D target, Vector3 gravity)
        {
            _cannon = cannon;
            _target = target;
            _gravity = gravity;
        }

        // (direction, timeOfCollision)
        public (Vector3, float, ParticleState3D) Aim()
        {
            var bestDir = Vector3.Zero;
            var bestTimeOfCollision = 9999f;
            var bestDistance = 9999f;
            var bestProjectile = new ParticleState3D(Vector3.Zero, Vector3.Zero, Vector3.Zero, 0f);

            for (int i = 0; i < ANGLES; ++i)
            {
                var dirProj = Vector3.Forward.Rotated(Vector3.Up, i * DELTA_ANGLE + DELTA_ANGLE / 2f);
                var axis = dirProj.Cross(Vector3.Up).Normalized();

                for (int j = 0; j < ANGLES; ++j)
                {
                    var dir = dirProj.Rotated(axis, j * DELTA_ANGLE + DELTA_ANGLE / 2f).Normalized();
                    var (timeOfCollision, distance, projectile) = FindClosestPointsOnTrajectory(dir);

                    if (distance < bestDistance)
                    {
                        bestDir = dir;
                        bestTimeOfCollision = timeOfCollision;
                        bestDistance = distance;
                        bestProjectile = projectile;
                    }
                }
            }

            //GD.Print(bestDir, " ", bestTimeOfCollision, " ", bestDistance);

            return (bestDir, bestTimeOfCollision, bestProjectile);
        }

        // (distance, timeOfCollision)
        private (float, float, ParticleState3D) FindClosestPointsOnTrajectory(Vector3 direction)
        {
            var projectile = _cannon.CreateProjectile(direction, _gravity);

            var curDirProj = _cannon.CurDirection - _cannon.CurDirection.Project(Vector3.Up);
            var directionProj = direction - direction.Project(Vector3.Up);

            float offT = (curDirProj.AngleTo(directionProj) + directionProj.AngleTo(direction)) / _cannon.AngularRotSpeed;

            var bestT = 9999f;
            var bestDistance = 9999f;

            for (int i = 0; i < ITERATIONS; ++i)
            {
                var t = DELTA_TIME * i;

                var p1 = projectile.ComputePosition(t);
                var p2 = _target.ComputePosition(t + offT);

                var distance = (p1 - p2).Length();

                if (distance < bestDistance)
                {
                    bestT = t;
                    bestDistance = distance;
                }
            }

            return (bestT, bestDistance, projectile);
        }

        private CannonState3D _cannon;
        private ParticleState3D _target;
        private Vector3 _gravity;
    }
}