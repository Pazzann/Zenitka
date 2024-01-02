using System;
using Godot;

namespace Zenitka.Scripts
{
	public static class MathUtils
	{
		private static Random _rng = new Random();

		public static float RandRange(float min, float max)
		{
			return _rng.NextSingle() * (max - min) + min;
		}

		public static Vector2 RandRange(in Vector2 min, in Vector2 max)
		{
			return new Vector2(
				RandRange(min.X, max.X),
				RandRange(min.Y, max.Y)
			);
		}

		public static Vector2 Min(Vector2 a, Vector2 b) {
			return new Vector2(Mathf.Min(a.X, b.X), Mathf.Min(a.Y, b.Y));
		}

		public static Vector2 Max(Vector2 a, Vector2 b) {
			return new Vector2(Mathf.Max(a.X, b.X), Mathf.Max(a.Y, b.Y));
		}

		public static Vector2 ProjectOnXZ(Vector3 v) {
			return new Vector2(v.X, v.Z);
		}

		public static bool IsCCW(Vector2 a, Vector2 b) {
			return a.Cross(b) > 0f;
		}

		public static float AngleDiff(float a, float b) {
			return Mathf.PosMod(Mathf.Abs(a - b), Mathf.Pi);
		}

		public static Transform3D Rotated(/*Vector3 point, */Vector3 axisPoint, Vector3 axisDirection, float angle) {
			//axisPoint = axisPoint + point.Project(axisDirection);

			return Transform3D.Identity
				.Translated(-axisPoint)
				.Rotated(axisDirection, angle)
				.Translated(axisPoint);
		}

		public static Vector3 ProjectOnPlane(Vector3 point, Vector3 normal) {
			return point - point.Project(normal);
		}

		public static Vector3 SafeNormalize(Vector3 v) {
			return v != Vector3.Zero ? v.Normalized() : Vector3.Zero;
		}

		public static Vector3 RandSphere() {
			var point = new Vector3(
				_rng.NextSingle(),
				_rng.NextSingle(),
				_rng.NextSingle()
			);

			return point.Normalized();
		}

		public static Vector3 OrientPlane(Vector3 v, Vector3 normal) {
			return v.Dot(normal) >= 0f ? v : -v;
		}
	}
}

