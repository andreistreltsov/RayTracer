
namespace RTIOW;

public abstract class Material
{
    public abstract (Ray ray, Color attenuation, bool scatter) ScatterRay(Ray r, HitResult.Hit hit);
}