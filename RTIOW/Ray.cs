namespace RTIOW;

public class Ray
{
    public Vector Origin { get; }
    public Vector Direction { get; }
    public Ray(Vector origin, Vector direction) => (Origin, Direction) = (origin, direction);

    public Vector At(double t)
    {
        return Origin + Direction * t;
    }

    private Color RenderBackground()
    {
        var white = new Color(1, 1, 1);
        var sky = new Color(0.5, 0.7, 1.0);
        var t = 0.5 * (Direction.Direction().Y + 1.0);
        return white * (1 - t) +  sky * t;
    }

    public Color Color(World world)
    {
        var hitResult = world.ComputeHit(this, 0, double.MaxValue);

        return hitResult switch
        {
            HitResult.Hit hit1 => new Color(hit1.Normal.X + 1, hit1.Normal.Y + 1, hit1.Normal.Z + 1) * 0.5,
            HitResult.Miss => RenderBackground(),
            _ => throw new ArgumentOutOfRangeException(nameof(hitResult))
        };
    }
}