namespace RTIOW;

public class Camera
{
    // Viewport dimensions in vector form
    private readonly Vector horizontal;
    private readonly Vector vertical;

    
    // Position vectors
    private readonly Vector lookFrom;
    private readonly Vector viewportLowerLeftCorner;

    public Camera(Vector lookFrom, Vector lookAt, Vector viewUp, double aspectRatio, double verticalFieldOfView)
    {
        this.lookFrom = lookFrom;

        var viewportHeight = 2.0 * Math.Tan(verticalFieldOfView/2.0) ;
        var viewportWidth = aspectRatio * viewportHeight;

        var w = (lookFrom - lookAt).Direction();
        var u = viewUp.Cross(w).Direction();
        var v = w.Cross(u);
        (horizontal, vertical) = (viewportWidth * u, viewportHeight * v);
        viewportLowerLeftCorner = lookFrom - (horizontal / 2) - (vertical / 2) - w;
    }

    public Ray GetRay(double s, double t)
    {
        return new Ray(lookFrom, viewportLowerLeftCorner + horizontal * s + vertical * t - lookFrom);
    }
}