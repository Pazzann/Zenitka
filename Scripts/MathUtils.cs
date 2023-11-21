
using System;
using System.Diagnostics;
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
	}
}

