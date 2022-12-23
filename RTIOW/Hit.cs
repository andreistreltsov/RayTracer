namespace RTIOW;

public record HitResult
{
    public record Miss : HitResult;

    public record Hit(Vector HitPoint, Vector Normal, double Distance, bool IsFrontFace) : HitResult;

    private HitResult()
    {
    }
}