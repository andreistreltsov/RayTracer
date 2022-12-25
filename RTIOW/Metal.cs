namespace RTIOW;

public class Metal : Material
{
    private readonly Color albedo;
    private readonly double fuzz;

    public Metal(Color albedo, double fuzz = 1.0) => (this.albedo, this.fuzz) = (albedo, fuzz);


    public override (Ray ray, Color attenuation, bool scatter) ScatterRay(Ray r, HitResult.Hit hit)
    {
        var reflected = r.Direction - 2 * r.Direction.Dot(hit.Normal) * hit.Normal;
        var fuzzed = reflected + fuzz * Vector.RandomPointInUnitSphere();

        return (new Ray(hit.HitPoint, fuzzed), 
            albedo, 
            reflected.Direction().SameDirection(hit.Normal));
    }
}