using Godot;

namespace Zenitka.Scripts._2D
{
    public struct CannonState2D
    {
        public Vector2 Position;
        public float BarrelLength, CurAngle, AngularRotSpeed, ProjectileSpeed, ProjectileLinearDrag;

        public CannonState2D(Vector2 position,
                             float barrelLength,
                             float curAngle,
                             float angularRotSpeed,
                             float projectileSpeed,
                             float projectileLinearDrag)
        {
            Position = position;
            BarrelLength = barrelLength;
            CurAngle = curAngle;
            AngularRotSpeed = angularRotSpeed;
            ProjectileSpeed = projectileSpeed;
            ProjectileLinearDrag = projectileLinearDrag;
        }

        public ParticleState2D CreateProjectile(float angle, Vector2 gravity) {
            var d = Vector2.FromAngle(angle);
            d.Y = -d.Y;

            return new ParticleState2D(
                Position + BarrelLength * d,
                ProjectileSpeed * d,
                gravity,
                ProjectileLinearDrag);
        }
    }

    public struct ParticleState2D
    {
        public Vector2 Position, Velocity, ConstantAcceleration;
        public float LinearDrag;

        public ParticleState2D(Vector2 position,
                           Vector2 velocity,
                           Vector2 constantAcceleration,
                           float linearDrag)
        {
            Position = position;
            Velocity = velocity;
            ConstantAcceleration = constantAcceleration;
            LinearDrag = linearDrag;
        }
        
        public Vector2 ComputePosition(float t)
        {
            return Position
                + ConstantAcceleration * t / LinearDrag
                - (ConstantAcceleration - LinearDrag * Velocity) / (LinearDrag * LinearDrag)
                + (ConstantAcceleration - LinearDrag * Velocity) * Mathf.Exp(-LinearDrag * t) / (LinearDrag * LinearDrag);
        }

        public Vector2 ComputeVelocity(float t)
        {
            return (ConstantAcceleration
                - (ConstantAcceleration - LinearDrag * Velocity) * Mathf.Exp(-LinearDrag * t)) / LinearDrag;
        }
    }

    public class Solver2D
    {
        private const int SECTORS = 500;
        private const int ITERATIONS = 400;

        private const float SECTOR_ARC = Mathf.Pi / SECTORS;
        private const float TIME_STEP = 0.02f; 

        private const float SCALE = 1f;

        private CannonState2D _cannon;
        private ParticleState2D _target;
        private Vector2 _gravity;

        public Solver2D(CannonState2D cannon, ParticleState2D target, Vector2 gravity) {
            _cannon = cannon;
            _target = target;
            _gravity = gravity;

            _target.ConstantAcceleration += _gravity;

            _cannon.BarrelLength /= SCALE;
            _cannon.Position /= SCALE;
            _cannon.ProjectileSpeed /= SCALE;

            _target.Position /= SCALE;
            _target.Velocity /= SCALE;
            _target.ConstantAcceleration /= SCALE;

            _gravity /= SCALE;
        }

        public (float, float) Aim() {
            var bestAngle = 0f;
            var bestTimeOfCollision = 9999f;
            var bestDistance = float.MaxValue;

            for (int i = 0; i < SECTORS; ++i) {
                float angle = i * SECTOR_ARC + SECTOR_ARC / 2f;
                var (distance, time) = ComputeClosestDistance(angle);

                if (distance < bestDistance) {
                    bestAngle = angle;
                    bestTimeOfCollision = time;
                    bestDistance = distance;
                }
            }

            //GD.Print("Best angle: ", bestAngle);
            //GD.Print("Best time: ", bestTimeOfCollision);
            //GD.Print("Best distance: ", bestDistance);

            return (bestAngle, bestTimeOfCollision);
        }

        // (distance, time)
        private (float, float) ComputeClosestDistance(float angle) {
            var projectile = _cannon.CreateProjectile(angle, _gravity);

            var bestT = 0f;
            var bestDistance = 9999f;

            for (int i = 0; i < ITERATIONS; ++i) {
                var t = TIME_STEP * i;

                var p1 = projectile.ComputePosition(t);
                var p2 = _target.ComputePosition(t + Mathf.Abs(angle - _cannon.CurAngle) / _cannon.AngularRotSpeed);
                
                //GD.Print(p1, " ", p2, " ", (p1 - p2).Length());

                var distance = (p1 - p2).Length();

                if (distance < bestDistance) {
                    bestT = t;
                    bestDistance = distance;
                }
            }

            return (bestDistance, bestT);
        }
    }
}