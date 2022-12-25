namespace RTIOW;

public class Glass : Material
{
    private readonly double indexOfRefraction;
    private readonly Random random = new();

    public Glass(double indexOfRefraction)
    {
        this.indexOfRefraction = indexOfRefraction;
        
    }

    private static Vector Refract(Vector incoming, Vector normal, double refractionRatio)
    {
        var cosTheta = Math.Min((-incoming).Dot(normal), 1.0);
        var refractedNormal= refractionRatio * (incoming + cosTheta * normal);
        var refractedTangential = -Math.Sqrt(Math.Abs(1.0 - refractedNormal.LengthSquared)) * normal;
        return refractedNormal + refractedTangential;
    }
    
    private static Vector Reflect(Vector incoming, Vector normal)
    {
        return incoming - 2 * incoming.Dot(normal) * normal;
    }

    private static double Reflectance(double cosTheta, double refractionRatio)
    {
        var r0 = Math.Pow((1 - refractionRatio) / (1 + refractionRatio), 2);
        return r0 + (1 - r0) * Math.Pow(1 - cosTheta, 5);
    }


    public override (Ray ray, Color attenuation, bool scatter) ScatterRay(Ray r, HitResult.Hit hit)
    {
        const double airIndexOfRefraction = 1.0;
        var refractionRatio = hit.IsFrontFace ? airIndexOfRefraction / indexOfRefraction : indexOfRefraction;
        var cosTheta = Math.Min((-r.Direction.Direction()).Dot(hit.Normal), 1.0);
        var sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);
        var cannotRefract = refractionRatio * sinTheta > 1.0;

        if (cannotRefract || Reflectance(cosTheta, refractionRatio) > random.NextDouble())
        {
            var reflectedRay = new Ray(hit.HitPoint, 
                Reflect(r.Direction.Direction(), hit.Normal));
            return (reflectedRay, new Color(1, 1, 1), true);
        }
        var refractedRay = new Ray(hit.HitPoint, 
            Refract(r.Direction.Direction(), hit.Normal, refractionRatio));
        return (refractedRay, new Color(1, 1, 1), true);

    }

    
}