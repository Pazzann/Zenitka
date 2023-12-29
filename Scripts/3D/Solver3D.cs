using Godot;
using static Zenitka.Scripts.MathUtils;

namespace Zenitka.Scripts._3D
{
	public struct SolverOptions
	{
		public int HIter = 12;
		public int VIter = 12;
		public int SimSteps = 400;
		public float SimTimeStep = 1f / 60f;

		public SolverOptions() { }
	}

	public class Solver3D
	{
		private const float INF = 9999f;
		private const float EPS = 1e-4f;

		private CannonState _cannon;
		private BodyState _target;
		private Vector3 _gravity;
		private SolverOptions _options;
		private int _projectileId;

		private readonly struct SimResult
		{
			public readonly float ColTime, AbsError;

			public SimResult(float colTime, float absError) { 
				ColTime = colTime;
				AbsError = absError;
			}
		}

		public struct SolverResult
		{
			public float HAngle, VAngle, ColTime, AbsError;

			public SolverResult(float hAngle, float vAngle, float colTime, float absError) {
				HAngle = hAngle;
				VAngle = vAngle;
				ColTime = colTime;
				AbsError = absError;
			}
		}

		public Solver3D(CannonState cannon, int projectileId, BodyState target, Vector3 gravity, SolverOptions options)
		{
			_cannon = cannon;
			_target = target;
			_gravity = gravity;
			_projectileId = projectileId;
			_options = options;
		}

		public SolverResult Aim()
		{
			var best = new SolverResult(_cannon.HAngle, _cannon.VAngle, 0f, INF);

			var d = ProjectOnXZ(_target.Position) - ProjectOnXZ(_cannon.HOrigin);
			var nDir = d - d.Project(ProjectOnXZ(_target.Velocity));
			var nHAngle = nDir.Angle();

			AimHRange(nHAngle - Mathf.Pi * 0.5f, nHAngle, ref best);
			AimHRange(nHAngle, nHAngle + Mathf.Pi * 0.5f, ref best);

			return best;
		}

		private void AimHRange(float minHAngle, float maxHAngle, ref SolverResult best)
		{
			for (int i = 0; i < _options.HIter && maxHAngle - minHAngle > EPS; ++i)
			{
				float hAngle1 = Mathf.Lerp(minHAngle, maxHAngle, 1f / 3f);
				var result1 = AimV(hAngle1, ref best);

				float hAngle2 = Mathf.Lerp(minHAngle, maxHAngle, 2f / 3f);
				var result2 = AimV(hAngle2, ref best);

				if (result1 < result2)
					maxHAngle = hAngle2;
				else
					minHAngle = hAngle1;

				if (result1 <= best.AbsError)
					best.HAngle = hAngle1;

				if (result2 <= best.AbsError)
					best.HAngle = hAngle1;
			}
		}

		private float AimV(float hAngle, ref SolverResult best)
		{
			float minVAngle = 0f, maxVAngle = Mathf.Pi / 2f;
			float bestAccuracy = INF;

			for (int i = 0; i < _options.VIter && maxVAngle - minVAngle > EPS; ++i)
			{
				float vAngle1 = Mathf.Lerp(minVAngle, maxVAngle, 1f / 3f);
				var result1 = Simulate(hAngle, vAngle1);

				float vAngle2 = Mathf.Lerp(minVAngle, maxVAngle, 2f / 3f);
				var result2 = Simulate(hAngle, vAngle2);

				if (result1.AbsError < result2.AbsError)
					maxVAngle = vAngle2;
				else
					minVAngle = vAngle1;

				bestAccuracy = Mathf.Min(bestAccuracy, result1.AbsError);
				bestAccuracy = Mathf.Min(bestAccuracy, result2.AbsError);

				if (result1.AbsError < best.AbsError)
				{
					best.AbsError = result1.AbsError;
					best.VAngle = vAngle1;
				}

				if (result2.AbsError < best.AbsError)
				{
					best.AbsError = result2.AbsError;
					best.VAngle = vAngle2;
				}
			}

			return bestAccuracy;
		}

		private SimResult Simulate(float hAngle, float vAngle)
		{
			var projectile = _cannon.CreateProjectile(_projectileId, hAngle, vAngle, _gravity);
			var result = new SimResult(0f, INF);

			var offT = AngleDiff(vAngle, _cannon.VAngle) / _cannon.VRotSpeed + AngleDiff(hAngle, _cannon.HAngle) / _cannon.HRotSpeed;

			Vector3 p1 = projectile.Position, p2 = _target.Position, v1 = projectile.Velocity, v2 = _target.Velocity;

			for (int i = 0; i < _options.SimSteps; ++i)
			{
				var t = _options.SimTimeStep * i;

				if (t >= offT)
					projectile.Integrate(ref p1, ref v1, _options.SimTimeStep);

				_target.Integrate(ref p2, ref v2, _options.SimTimeStep);

				var absError = (p1 - p2).Length();

				if (absError < result.AbsError)
					result = new SimResult(t, absError);
			}

			return result;
		}
	}
}
