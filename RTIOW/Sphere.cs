namespace RTIOW;

internal class Sphere: Entity
{
    private readonly Vector center;
    private readonly double radius;

    public Sphere(Vector center, double radius)
    {
        this.center = center;
        this.radius = radius;
    }
    
    public override HitResult ComputeHit(Ray r, double minDistance, double maxDistance)
    {
        var oc = r.Origin - center;
        var a = r.Direction.LengthSquared;
        var b = (r.Direction * 2).Dot(oc);
        var c = oc.Dot(oc) - radius * radius;
        var discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return new HitResult.Miss();
        }

        var distanceToHitPoint = (-b - Math.Sqrt(discriminant)) / (2.0 * a);
        if (distanceToHitPoint < minDistance || distanceToHitPoint > maxDistance)
        {
            distanceToHitPoint = (-b + Math.Sqrt(discriminant)) / (2.0 * a);
            if (distanceToHitPoint < minDistance || distanceToHitPoint > maxDistance)
            {
                return new HitResult.Miss();
            }
        }

        var hitPoint = r.At(distanceToHitPoint);
        var outwardNormal = (hitPoint - center).Direction();
        var isFrontFace = outwardNormal.Dot(r.Direction) < 0.0;
        
        return new HitResult.Hit(hitPoint, 
            isFrontFace ? outwardNormal : -outwardNormal, 
            distanceToHitPoint, 
            isFrontFace);
        
    }
}