namespace RTIOW;

public abstract class Material
{
    public abstract (Ray ray, Color attenuation, bool scatter) ScatterRay(Ray r, HitResult.Hit hit);
}

public class Metal : Material
{
    private readonly Color albedo;
    private double fuzz;

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

public class Diffuse: Material
{
    private readonly Color albedo;
    
    public Diffuse(Color albedo) => this.albedo = albedo;
    
    public override (Ray ray, Color attenuation, bool scatter) ScatterRay(Ray incoming, HitResult.Hit hit)
    {
        var scatterDirection = hit.Normal + Vector.RandomPointOnUnitSphereSurface();

        // Discard near-zero scattered vectors.
        if (scatterDirection.Length < 1e-8)
        {
            scatterDirection = hit.Normal;
        }
        
        var reflectedRay = new Ray(hit.HitPoint, scatterDirection);
        return (reflectedRay, albedo, true);
    }
}

