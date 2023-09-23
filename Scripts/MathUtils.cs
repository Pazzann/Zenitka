
using System;
using System.Diagnostics;
using Godot;

namespace Zenitka.Scripts
{
    public static class MathUtils {
        private static Random _rng = new Random();

        public static float RandRange(float min, float max) {
            return _rng.NextSingle() * (max - min) + min;
        }

        public static Vector2 RandRange(in Vector2 min, in Vector2 max) {
            return new Vector2(
                RandRange(min.X, max.X),
                RandRange(min.Y, max.Y)
            );
        }

        public static float CalculateFiringAngle(
            float barrelLength,
            float muzzleSpeed,
            in Vector2 targetPos,
            in Vector2 targetVelocity
        ) {
            float t = CalculateHitTime(barrelLength, muzzleSpeed, targetPos, targetVelocity);

            if (t == 0)
                return float.MaxValue;

            float cosA = (targetPos.X + targetVelocity.X * t) / (barrelLength + muzzleSpeed * t);
            return Mathf.Acos(cosA);
        }

        public static (float, float) CalculateFiringAngle3D(
            float barrelLength,
            float muzzleSpeed,
            in Vector3 targetPos,
            in Vector3 targetVelocity,
            in Vector3 canonPos
        )
        {
            float t = CalculateHitTime3D(barrelLength, muzzleSpeed, targetPos, targetVelocity, canonPos);
            if (t == 0)
                return (float.MaxValue, float.MaxValue);

            float cosA = (targetPos.X + targetVelocity.X * t) / (barrelLength + muzzleSpeed * t);
            float cosB = (targetPos.Y + targetVelocity.Y * t) / (barrelLength + muzzleSpeed * t);
            var a = (Mathf.Acos(cosA), Mathf.Acos(cosB));
            return a;
        }
        

        public static float CalculateHitTime3D(
            float barrelLength,
            float muzzleSpeed,
            in Vector3 targetPos,
            in Vector3 targetVelocity,
            in Vector3 canonPos
        )
        {
            var c_1 = targetPos[0] - canonPos[0];
            var c_2 = targetPos[1] - canonPos[1];
            var c_3 = targetPos[2] - canonPos[2];
            var v_b = muzzleSpeed;

            var a = Mathf.Pow(targetVelocity[0], 2) + Mathf.Pow(targetVelocity[1], 2) + Mathf.Pow(targetVelocity[2], 2) - Mathf.Pow(v_b, 2);
            var b = 2 * (targetVelocity[0] * c_1 + targetVelocity[1] * c_2 + targetVelocity[2] * c_3);
            var c = Mathf.Pow(c_1 , 2) + Mathf.Pow(c_2, 2) + Mathf.Pow(c_3, 2);
            
            var d = b * b - 4 * a * c;
            if (d < 0f)
                return 0f;
            
            var (t1, t2) = SolveQuadricWithDeterminant(a, b, d);
            var t = Mathf.Min(t1, t2);
            
            if (t1 > 0f && t2 > 0f)
                return Mathf.Min(t1, t2);
            else if (t1 > 0f)
                return t1;
            else if (t2 > 0f)
                return t2;
            else
                return 0;
        }

        private static float CalculateHitTime(
            float barrelLength,
            float muzzleSpeed,
            in Vector2 targetPos,
            in Vector2 targetVelocity
        ) {
            var a = targetVelocity.LengthSquared() - muzzleSpeed * muzzleSpeed;
            var b = 2f * (targetPos.Dot(targetVelocity) - barrelLength * muzzleSpeed);
            var c = targetPos.LengthSquared() - barrelLength * barrelLength;

            var d = b * b - 4 * a * c;

            if (d < 0f)
                return 0;

            var (t1, t2) = SolveQuadricWithDeterminant(a, b, d);

            if (t1 > 0f && t2 > 0f)
                return Mathf.Min(t1, t2);
            else if (t1 > 0f)
                return t1;
            else if (t2 > 0f)
                return t2;
            else
                return 0;
        }

        private static (float, float) SolveQuadricWithDeterminant(float a, float b, float d) {
            d = Mathf.Sqrt(d);
            return ((-b - d) / (2f * a), (-b + d) / (2f * a));
        }
    }
} 

