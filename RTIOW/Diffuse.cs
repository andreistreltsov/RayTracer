namespace RTIOW;

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