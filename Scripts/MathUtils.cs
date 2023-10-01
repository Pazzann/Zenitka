
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
			float projectileSpeed,
			in Vector2 targetPos,
			in Vector2 targetVelocity
		) {
			float t = CalculateHitTime(barrelLength, projectileSpeed, targetPos, targetVelocity);

			if (t == 0)
				return float.MaxValue;

			float cosA = (targetPos.X + targetVelocity.X * t) / (barrelLength + projectileSpeed * t);
			return Mathf.Acos(cosA);
		}

		public static Vector3 CalculateCannonDirection3D(
			float barrelLength,
			float projectileSpeed,
			in Vector3 targetPos,
			in Vector3 targetVelocity
		) {
			float t = CalculateHitTime3D(barrelLength, projectileSpeed, targetPos, targetVelocity);
			GD.Print("t: ", t);

			if (t == 0)
				return new Vector3(0f, 1f, 0f);

			//GD.Print("aboba ", (targetPos + targetVelocity * t) / (targetPos + targetVelocity * t).Normalized() * projectileSpeed * t);

			return (targetPos + targetVelocity * t).Normalized();
		}

		private static float CalculateHitTime3D(
			float barrelLength,
			float projectileSpeed,
			in Vector3 targetPos,
			in Vector3 targetVelocity
		) {
			var a = targetVelocity.LengthSquared() - projectileSpeed * projectileSpeed;
			var b = 2f * (targetPos.Dot(targetVelocity) - barrelLength * projectileSpeed);
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

