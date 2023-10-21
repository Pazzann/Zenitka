using System.Reflection.Metadata;
using System.Threading.Tasks;
using Godot;
using Microsoft.VisualBasic;

namespace Zenitka.Scripts._3D
{
    public struct ParticleState3D
    {
        public ParticleState3D(Vector3 position, Vector3 velocity, Vector3 constantAcceleration, float linearDrag)
        {
            Position = position;
            Velocity = velocity;
            ConstantAcceleration = constantAcceleration;
            LinearDrag = linearDrag;
        }

        public Vector3 ComputePosition(float t)
        {
            return Position
                + ConstantAcceleration * t / LinearDrag
                - (ConstantAcceleration - LinearDrag * Velocity) / (LinearDrag * LinearDrag)
                + (ConstantAcceleration - LinearDrag * Velocity) * Mathf.Exp(-LinearDrag * t) / (LinearDrag * LinearDrag);
        }

        public Vector3 ComputeVelocity(float t)
        {
            return (ConstantAcceleration
                - (ConstantAcceleration - LinearDrag * Velocity) * Mathf.Exp(-LinearDrag * t)) / LinearDrag;
        }

        public Vector3 Position, Velocity, ConstantAcceleration;
        public float LinearDrag;
    }

    public struct CannonState3D
    {
        public CannonState3D(
            Vector3 projectileSpawnPosition,
            Vector3 curDirection,
            float angularRotSpeed,
            float projectileSpeed,
            float projectileAcceleration,
            float projectileLinearDrag)
        {
            ProjectileSpawnPosition = projectileSpawnPosition;
            CurDirection = curDirection.Normalized();
            AngularRotSpeed = angularRotSpeed;
            ProjectileSpeed = projectileSpeed;
            ProjectileAcceleration = projectileAcceleration;
            ProjectileLinearDrag = projectileLinearDrag;
        }

        // direction vector must be normalized
        public ParticleState3D CreateProjectile(in Vector3 direction, in Vector3 gravity)
        {
            return new ParticleState3D(
                ProjectileSpawnPosition,
                ProjectileSpeed * direction,
                ProjectileAcceleration * direction + gravity,
                ProjectileLinearDrag);
        }

        public Vector3 ProjectileSpawnPosition, CurDirection;
        public float AngularRotSpeed, ProjectileSpeed, ProjectileAcceleration, ProjectileLinearDrag;
    }

    public class Solver3D {
        private const int ANGLES = 100;
        private const int ITERATIONS = 100;

        private const float DELTA_ANGLE = Mathf.Pi / ANGLES;
        private const float DELTA_TIME = 0.05f;

        public Solver3D(CannonState3D cannon, ParticleState3D target, Vector3 gravity) {
            _cannon = cannon;
            _target = target; 
            _gravity = gravity;
        }

        // (direction, timeOfCollision)
        public (Vector3, float) Aim() {
            var bestDir = Vector3.Zero;
            var bestTimeOfCollision = 9999f;
            var bestDistance = 9999f;

            for (int i = 0; i < ANGLES; ++i) {
                var dirProj = Vector3.Forward.Rotated(Vector3.Up, i * DELTA_ANGLE + DELTA_ANGLE / 2f);
                var axis = dirProj.Cross(Vector3.Up).Normalized();

                for (int j = 0; j < ANGLES; ++j) {
                    var dir = dirProj.Rotated(axis, j * DELTA_ANGLE + DELTA_ANGLE / 2f).Normalized();
                    var (timeOfCollision, distance) = FindClosestPointsOnTrajectory(dir);

                    if (distance < bestDistance) {
                        bestDir = dir;
                        bestTimeOfCollision = timeOfCollision;
                        bestDistance = distance;
                    }
                }
            }

            return (bestDir, bestTimeOfCollision);
        }

        // (distance, timeOfCollision)
        private (float, float) FindClosestPointsOnTrajectory(Vector3 direction) {
            var projectile = _cannon.CreateProjectile(direction, _gravity);
            float offT = direction.AngleTo(_cannon.CurDirection) / _cannon.AngularRotSpeed;

            var bestT = 9999f;
            var bestDistance = 9999f;

            for (int i = 0; i < ITERATIONS; ++i) {
                var t = DELTA_TIME * i;

                var p1 = projectile.ComputePosition(t);
                var p2 = _target.ComputePosition(t + offT);

                var distance = (p1 - p2).Length();

                if (distance < bestDistance) {
                    bestT = t;
                    bestDistance = distance;
                }
            }

            return (bestT, bestDistance);
        }

        private CannonState3D _cannon;
        private ParticleState3D _target;
        private Vector3 _gravity;
    }
}