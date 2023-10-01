using System.Collections.Generic;
using Godot;

namespace Zenitka.Scripts.Math
{
    public class Math2D
    {
        public static float GetAngle(
            Vector2 gunPosition,
            Vector2 startTargetPosition,
            float gravitationalAcceleration,
            float bulletWeight,
            float targetWeight,
            float rotationalSpeed,
            float startBulletVelocity,
            float startTargetVelocity,
            float startTargetAngle,
            float initialGunAngle,
            
            float airDensity,
            float bulletSurface,
            float targetSurface,
            float bulletDragCoefficient,
            float targetDragCoefficient,
            
            float precision,
            Vector2 calculationSize
            
            )
        {
            float bulletStiffness = bulletDragCoefficient / bulletWeight;
            float targetStiffness = targetDragCoefficient / targetWeight;

            float startTargetVelocityX = startTargetVelocity * (float)System.Math.Cos(startTargetAngle);
            float startTargetVelocityY = startTargetVelocity * (float)System.Math.Sin(startTargetAngle);
            
            
            List<float[]> times = new List<float[]>();

            for (float alpha = -(float)(System.Math.PI / 2); alpha < (float)(System.Math.PI / 2); alpha += 0.05f)
            {
                float startBulletVelocityX = startBulletVelocity * (float)System.Math.Cos(alpha);
                float startBulletVelocityY = startBulletVelocity * (float)System.Math.Sin(alpha);
                
                List<Vector2> intercepts = new List<Vector2>();
                
                for (float x = 0.0f; x < calculationSize.X; x += precision)
                {
                    float targetTrajectory = (targetStiffness * (gravitationalAcceleration * (x - startTargetPosition.X) + targetStiffness * (x * startTargetVelocityY - startTargetVelocityY * startTargetPosition.X + startTargetVelocityX * startTargetPosition.Y)) - gravitationalAcceleration * startTargetVelocityX * (float)System.Math.Log(startTargetVelocityX / (startTargetVelocityX - x * targetStiffness + startTargetPosition.X * targetStiffness)))/(startTargetVelocityX * targetStiffness * targetStiffness);
                    float bulletTrajectory = (startBulletVelocity * (gunPosition.Y * bulletStiffness * bulletStiffness - gravitationalAcceleration * (float)System.Math.Log(startBulletVelocityX/(- x * bulletStiffness + gunPosition.X * bulletStiffness + startBulletVelocityX))) + (x - gunPosition.X) * bulletStiffness * (float) (1/System.Math.Cos(alpha)) * (gravitationalAcceleration + startBulletVelocityY * bulletStiffness)) / (startBulletVelocity * bulletStiffness * bulletStiffness);

                    if (System.Math.Abs(targetTrajectory - bulletTrajectory) < precision)
                        intercepts.Add(new Vector2(x, (float)System.Math.Round(targetTrajectory, 1)));
                }

                foreach (var intercept in intercepts)
                {
                    float targetTime = (float)System.Math.Log(startTargetVelocityX/ (startTargetVelocityX - intercept.X * targetStiffness + startTargetPosition.X * targetStiffness))/ targetStiffness;
                    float bulletTime = (float)System.Math.Log(startBulletVelocityX/ (startBulletVelocityX - intercept.X * bulletStiffness + gunPosition.X * bulletStiffness))/ bulletStiffness;

                    if (System.Math.Abs(targetTime - (bulletTime + System.Math.Abs((initialGunAngle - alpha)/rotationalSpeed))) < precision)
                        times.Add(new float[] {targetTime + (bulletTime + (float)System.Math.Abs((initialGunAngle - alpha) / rotationalSpeed)) / 2.0f , alpha});
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

       
    }
}