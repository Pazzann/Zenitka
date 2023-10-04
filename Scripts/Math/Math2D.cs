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

            for (float alpha = 0; alpha < (float)(System.Math.PI); alpha += 0.02f)
            {
                // float alpha = (3.0f / 4.0f) * (float)System.Math.PI;

                List<Vector2> intercepts = new List<Vector2>();
                
                
                for (float x = -calculationSize.X / 2; x < calculationSize.X / 2; x += precision)
                {
                    // GD.Print("New iteration: x: ", x);
                    float targetTrajectory = YPositionFromX(target, gravitationalAcceleration, x, startTargetPosition,
                        target.StartAngle);
                    // GD.Print("Target:", targetTrajectory);
                    float bulletTrajectory = YPositionFromX(bullet, gravitationalAcceleration, x, gunPosition, -alpha);
                    // GD.Print("Bullet:", bulletTrajectory);
                    if (System.Math.Abs(targetTrajectory - bulletTrajectory) < precision)
                        intercepts.Add(new Vector2(x, (float)System.Math.Round(targetTrajectory, 1)));
                }

                GD.Print(intercepts.Count);
                foreach (var intercept in intercepts)
                {
                    float targetTime = TFromX(target, intercept.X, startTargetPosition, target.StartAngle);
                    float bulletTime = TFromX(bullet, intercept.X, gunPosition , alpha);
                    // GD.PrintT(targetTime);
                    // GD.PrintT(bulletTime);

                    if (System.Math.Abs(targetTime -
                                        (bulletTime + System.Math.Abs(((0.5f * Mathf.Pi - initialGunAngle) - alpha) /
                                                                      rotationalSpeed))) < 0.05f)
                    // if (System.Math.Abs(targetTime - (bulletTime)) < 0.2f)
                        times.Add(new float[]
                        {
                            targetTime,
                            alpha
                        });
                }
            }

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

        public static float YPositionFromX(Target target, float gravitationalAcceleration, float x, Vector2 startPos,
            float angle)
        {
            float startVelocityX =
                target.StartVelocity * (float)System.Math.Cos(angle);
            float startVelocityY =
                target.StartVelocity * (float)System.Math.Sin(angle);

            float stiffness = target.DragCoefficient / target.Weight;

            // GD.Print(startVelocityX + " " + startVelocityY + " " + stiffness + " " + startPos.X + " " + startPos.Y);
            return ((stiffness * (gravitationalAcceleration * (x - startPos.X) + stiffness *
                         (x * startVelocityY - startVelocityY * startPos.X + startVelocityX * startPos.Y)) -
                     gravitationalAcceleration * startVelocityX *
                     (float)System.Math.Log(startVelocityX /
                                            (startVelocityX - x * stiffness + startPos.X * stiffness))) /
                    (startVelocityX * stiffness * stiffness));
        }

        public static float TFromX(Target target, float x, Vector2 startPos, float alpha)
        {
            float startVelocityX =
                target.StartVelocity * (float)System.Math.Cos(alpha);
            float stiffness = target.DragCoefficient / target.Weight;
            return (float)System.Math.Log(startVelocityX / (startVelocityX -
                x * stiffness + startPos.X * stiffness)) / stiffness;
        }
    }
}