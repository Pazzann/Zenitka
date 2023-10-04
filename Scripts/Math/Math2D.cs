using System.Collections.Generic;
using Godot;
using Zenitka.Scripts._2D;

namespace Zenitka.Scripts.Math
{
	public class Math2D
	{
		public static float GetAngle(
			Vector2 gunPosition,
			Vector2 startTargetPosition,
			Target bullet,
			Target target,
			float gravitationalAcceleration,
			float rotationalSpeed,
			float initialGunAngle,
			float precision,
			Vector2 calculationSize
		)
		{
			List<float[]> times = new List<float[]>();

			// for (float alpha = 0.0f; alpha < (float)(System.Math.PI); alpha += 0.05f)
			// {
			float alpha = (3.0f / 4.0f) * (float)System.Math.PI;


			List<Vector2> intercepts = new List<Vector2>();

			for (float x = -calculationSize.X / 2; x < calculationSize.X / 2; x += precision)
			{
				GD.Print("New iteration: x: ", x);
				float targetTrajectory = YPositionFromX(target, gravitationalAcceleration, x, startTargetPosition);
				GD.Print("Target:", targetTrajectory);
				float bulletTrajectory = YPositionFromX(bullet, gravitationalAcceleration, x, gunPosition);
				GD.Print("Bullet:", bulletTrajectory);
				if (System.Math.Abs(targetTrajectory - bulletTrajectory) < precision)
					intercepts.Add(new Vector2(x, (float)System.Math.Round(targetTrajectory, 1)));
			}

			GD.Print(intercepts[0].X);
			foreach (var intercept in intercepts)
			{
				float targetTime = TFromX(target, gravitationalAcceleration, startTargetPosition);
				//float targetTime = TFromX(target, gravitationalAcceleration, intercept.X, startTargetPosition); изменил для
				//запуска 3Д
				float bulletTime = TFromX(bullet, gravitationalAcceleration, gunPosition);
				//float bulletTime = TFromX(bullet, gravitationalAcceleration, intercept.X, gunPosition);
				//изменил для запyска 3Д
				GD.PrintT(targetTime);
				GD.PrintT(bulletTime);

				if (System.Math.Abs(targetTime -
									(bulletTime + System.Math.Abs(((0.5f * Mathf.Pi - initialGunAngle) - alpha) /
																  rotationalSpeed))) < precision)
					times.Add(new float[]
					{
						targetTime,
						alpha
					});
			}
			// }

			if (times.Count == 0)
				return 0.0f;

			float minTime = 9999999.0f;
			float finalAlpha = 0.0f;
			foreach (var time in times)
			{
				if (time[0] < minTime)
				{
					minTime = time[0];
					finalAlpha = time[1];
				}
			}


			return finalAlpha;
		}

		public static float XVelocityFromT(Target obj, float t)
		{
			float startTargetVelocityX = obj.StartVelocity * (float)System.Math.Cos(obj.StartAngle);
			float stiffness = obj.DragCoefficient / obj.Weight;
			return (float)System.Math.Exp((-1) * t * (double)stiffness) * startTargetVelocityX;
		}

		public static float YVelocityFromT(Target obj, float t, float gravitationalAcceleration)
		{
			float startTargetVelocityY = obj.StartVelocity * (float)System.Math.Sin(obj.StartAngle);
			float stiffness = obj.DragCoefficient / obj.Weight;
			return (-1) * ((-1) * gravitationalAcceleration * stiffness +
						   (float)System.Math.Exp((-1) * t * (double)stiffness) * stiffness *
						   (gravitationalAcceleration + startTargetVelocityY * stiffness)) / (stiffness * stiffness);
		}

		public static float YPositionFromX(Target target, float gravitationalAcceleration, float x, Vector2 startPos)
		{
			float startVelocityX =
				target.StartVelocity * (float)System.Math.Cos(0.5f * Mathf.Pi - target.StartAngle);
			float startVelocityY =
				target.StartVelocity * (float)System.Math.Sin(0.5f * Mathf.Pi - target.StartAngle);

			float stiffness = target.DragCoefficient / target.Weight;
			return ((stiffness * (gravitationalAcceleration * (x - startPos.X) + stiffness *
						 (x * startVelocityY - startVelocityY * startPos.X + startVelocityX * startPos.Y)) -
					 gravitationalAcceleration * startVelocityX *
					 (float)System.Math.Log(startVelocityX / (startVelocityX - x * stiffness + startPos.X * stiffness))) /
					(startVelocityX * stiffness * stiffness));
		}

		public static float TFromX(Target target, float x, Vector2 startPos)
		{
			float startVelocityX =
				target.StartVelocity * (float)System.Math.Cos(0.5f * Mathf.Pi - target.StartAngle);
			float stiffness = target.DragCoefficient / target.Weight;
			return (float)System.Math.Log(startVelocityX / (startVelocityX -
				x * stiffness + startPos.X * stiffness)) / stiffness;
		}
	}
}
