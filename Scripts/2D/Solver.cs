using Godot;

namespace Zenitka.Scripts._2D;

public record SolverOptions(
    int SimSteps = 400,
    float SimTimeStep = 1f / 60f,
    int MaxIter = 20,
    float AngleEps = 1e-5f);

public readonly record struct SimResult(float AbsError, float ColTime, Vector2 ColPosDiff);

public readonly record struct AimResult(float Angle, float AbsError, float ColTime);

public class CannonSolver(Cannon cannon, BallisticBody target, SolverOptions options)
{
    private const float Inf = 9999f;

    public AimResult Aim()
    {
        var result1 = Aim(0.1f, Mathf.Pi / 2f, false);
        var result2 = Aim(Mathf.Pi / 2f, Mathf.Pi - 0.1f, true);

        return result1.AbsError <= result2.AbsError ? result1 : result2;
    }

    private AimResult Aim(float minAngle, float maxAngle, bool mirrored)
    {
        var angle = 0f;
        var result = new SimResult(Inf, 0f, Vector2.Zero);

        for (int i = 0; i < options.MaxIter && maxAngle - minAngle > options.AngleEps; ++i)
        {
            angle = (minAngle + maxAngle) / 2f;
            result = Simulate(angle);

            if ((result.ColPosDiff.Y > 0) ^ mirrored)
                maxAngle = angle;
            else if ((result.ColPosDiff.Y < 0) ^ mirrored)
                minAngle = angle;
        }

        return new AimResult(angle, result.AbsError, result.ColTime);
    }

    private SimResult Simulate(float angle)
    {
        var body = new PBody(cannon.ProjectileProps.PBody, cannon.NewProjectileState(angle));
        var delay = cannon.MeasureDelay(angle);

        return Solver.Simulate(body, target.PBody, options.SimSteps, options.SimTimeStep, delay);
    }
}

public static class Solver
{
    private const float Inf = 9999f;
    
    public static SimResult Simulate(PBody body, PBody target, int steps, float timeStep, float delay)
    {
        var result = new SimResult(Inf, 0f, Vector2.Zero);

        for (var i = 0; i < steps; ++i)
        {
            var t = timeStep * i;

            if (t >= delay)
                body.State = body.State.Update(timeStep, body.Props);

            target.State = target.State.Update(timeStep, target.Props);

            var posDiff = target.State.Position - body.State.Position;
            var absError = posDiff.Length();

            if (absError < result.AbsError)
                result = new SimResult(absError, t, posDiff);
        }

        return result;
    }
}