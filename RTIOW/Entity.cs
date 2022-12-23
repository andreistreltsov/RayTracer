namespace RTIOW;

public abstract class Entity
{
    public abstract HitResult ComputeHit(Ray r, double minDistance, double maxDistance);
}