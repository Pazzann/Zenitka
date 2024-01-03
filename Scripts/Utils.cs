using System;
using System.Threading.Tasks;
using Godot;

namespace Zenitka.Scripts
{
    public static class Utils
    {
        public static readonly Random Rng = new();

        public static float RandRange(float min, float max)
        {
            return Rng.NextSingle() * (max - min) + min;
        }

        public static Vector2 ProjectOnXZ(Vector3 v)
        {
            return new Vector2(v.X, v.Z);
        }

        public static float AngleDiff(float a, float b)
        {
            return Mathf.PosMod(Mathf.Abs(a - b), Mathf.Pi);
        }

        public static Transform3D Rotated(Vector3 axisPoint, Vector3 axisDirection, float angle)
        {
            return Transform3D.Identity
                .Translated(-axisPoint)
                .Rotated(axisDirection, angle)
                .Translated(axisPoint);
        }

        public static Vector3 ProjectOnPlane(Vector3 point, Vector3 normal)
        {
            return point - point.Project(normal);
        }

        public static Vector3 SafeNormalize(Vector3 v)
        {
            return v != Vector3.Zero ? v.Normalized() : Vector3.Zero;
        }

        public static Vector3 RandUnitSphere()
        {
            var point = new Vector3(
                Rng.NextSingle(),
                Rng.NextSingle(),
                Rng.NextSingle()
            );

            return point.Normalized();
        }

        public static Vector3 OrientPlane(Vector3 v, Vector3 normal)
        {
            return v.Dot(normal) >= 0f ? v : -v;
        }

        public static Task AwaitTween(Tween tween)
        {
            var tcs = new TaskCompletionSource();
            tween.TweenCallback(Callable.From(() => tcs.TrySetResult()));
            return tcs.Task;
        }
    }
}