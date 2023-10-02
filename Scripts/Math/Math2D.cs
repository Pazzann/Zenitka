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
            float bulletStiffness = bullet.DragCoefficient / bullet.Weight;
            float targetStiffness = target.DragCoefficient / target.Weight;
   

            float startTargetVelocityX =
                target.StartVelocity * (float)System.Math.Cos(0.5f * Mathf.Pi - target.StartAngle);
            float startTargetVelocityY =
                target.StartVelocity * (float)System.Math.Sin(0.5f * Mathf.Pi - target.StartAngle);


            List<float[]> times = new List<float[]>();
            GD.Print("Started");
            for (float alpha = (float)(System.Math.PI / 2); alpha < (float)(System.Math.PI - System.Math.PI / 4); alpha += 0.05f)
            {
                float startBulletVelocityX = bullet.StartVelocity * (float)System.Math.Cos(alpha);
                float startBulletVelocityY = bullet.StartVelocity * (float)System.Math.Sin(alpha);
             

                List<Vector2> intercepts = new List<Vector2>();

                for (float x = -calculationSize.X / 2; x < calculationSize.X / 2; x += precision)
                {
                    float targetTrajectory =
                        ((targetStiffness * (gravitationalAcceleration * (x - startTargetPosition.X) +
                                                    targetStiffness *
                                                    (x * startTargetVelocityY -
                                                     startTargetVelocityY * startTargetPosition.X +
                                                     startTargetVelocityX * startTargetPosition.Y)) -
                                 gravitationalAcceleration *
                                 startTargetVelocityX
                                 *
                                 (float)System.Math.Log(startTargetVelocityX / (startTargetVelocityX -
                                     x * targetStiffness +
                                     startTargetPosition.X * targetStiffness))) /
                                (startTargetVelocityX * targetStiffness * targetStiffness));
                    GD.Print(targetTrajectory);
                    float bulletTrajectory =
                        ((bullet.StartVelocity * (gunPosition.Y * bulletStiffness * bulletStiffness -
                                                         gravitationalAcceleration *
                                                         (float)System.Math.Log(startBulletVelocityX /
                                                             (-x * bulletStiffness +
                                                              gunPosition.X * bulletStiffness +
                                                              startBulletVelocityX))) +
                                 (x - gunPosition.X) * bulletStiffness * (float)(1 / System.Math.Cos(alpha)) *
                                 (gravitationalAcceleration + startBulletVelocityY * bulletStiffness)) /
                                (bullet.StartVelocity * bulletStiffness * bulletStiffness));
                    GD.Print(bulletTrajectory);
                    if (System.Math.Abs(targetTrajectory - bulletTrajectory) < precision)
                        intercepts.Add(new Vector2(x, (float)System.Math.Round(targetTrajectory, 1)));
                    GD.Print("ADOLLFFFFFFF");
                }

                foreach (var intercept in intercepts)
                {
                    float targetTime = (float)System.Math.Log(startTargetVelocityX / (startTargetVelocityX -
                        intercept.X * targetStiffness + startTargetPosition.X * targetStiffness)) / targetStiffness;
                    float bulletTime = (float)System.Math.Log(startBulletVelocityX / (startBulletVelocityX -
                        intercept.X * bulletStiffness + gunPosition.X * bulletStiffness)) / bulletStiffness;

                    if (System.Math.Abs(targetTime -
                                        (bulletTime + System.Math.Abs(((0.5f * Mathf.Pi - initialGunAngle) - alpha) /
                                                                      rotationalSpeed))) <
                        precision)
                        times.Add(new float[]
                        {
                            targetTime +
                            (bulletTime +
                             (float)System.Math.Abs(((0.5f * Mathf.Pi - initialGunAngle) - alpha) / rotationalSpeed)) /
                            2.0f,
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
    }
}