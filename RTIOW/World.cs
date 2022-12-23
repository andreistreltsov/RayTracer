namespace RTIOW;

public class World
{
    private readonly List<Entity> entities = new();

    public World(IEnumerable<Entity> entities)
    {
        this.entities.AddRange(entities);
    }

    public HitResult ComputeHit(Ray r, double minDistance, double maxDistance)
    {
        var tMin = double.PositiveInfinity;
        HitResult closestHitResult = new HitResult.Miss();
        
        foreach (var e in entities)
        {
            var hitResult = e.ComputeHit(r, minDistance, maxDistance);

            if (hitResult is HitResult.Hit hit)
            {
                if (hit.Distance < tMin)
                {
                    closestHitResult = hit;
                    tMin = hit.Distance;
                }
            }
        }
        return closestHitResult;
    }
}