namespace RTIOW;

class Camera
{
    // Viewport dimensions in vector form
    private readonly Vector horizontal;
    private readonly Vector vertical;
    
    // Position vectors
    private readonly Vector origin;
    private readonly Vector viewportLowerLeftCorner;

    public Camera(Vector origin, double aspectRatio)
    {
        this.origin = origin;
        const double viewportHeight = 2.0;
        const double focalLength = 1.0;
        var viewportWidth = aspectRatio * viewportHeight;
        horizontal = new Vector(viewportWidth, 0, 0);
        vertical = new Vector(0, viewportHeight, 0);
        viewportLowerLeftCorner = origin - (horizontal / 2) - (vertical / 2) - new Vector(0, 0, focalLength);
    }

    public Ray GetRay(double u, double v)
    {
        return new Ray(origin, viewportLowerLeftCorner + horizontal * u + vertical * v - origin);
    }
}