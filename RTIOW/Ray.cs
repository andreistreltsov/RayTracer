namespace RTIOW;

public class Ray
{
    private const int MaxDepth = 50;
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
    

    public Color Color(Scene scene, int depth=0)
    {
        if (depth >= MaxDepth)
        {
            return new Color();
        }
        
        var hitResult = scene.ComputeHit(this, 0.001, double.MaxValue);

        switch (hitResult)
        {
            case HitResult.Miss:
                return RenderBackground();
            case HitResult.Hit hit:
                // compute the diffuse reflected ray and get its color
                var (reflectedRay, attenuation, scatter) = hit.Material.ScatterRay(this, hit);
                if (scatter)
                {
                    return attenuation * reflectedRay.Color(scene, depth + 1);
                }
                return new Color();
            default:
                throw new ArgumentOutOfRangeException(nameof(hitResult));
        }

    }

}