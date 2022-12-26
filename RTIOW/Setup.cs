namespace RTIOW;

public static class Setup
{
    public const double TimeStart = 3; 
    public const double TimeEnd = 15; 
    public const int FPS = 30; 
    public static Func<double, Camera> CameraAnimation(double aspectRatio)
    {
        return t =>
        {
            var angFreq = Math.PI / 4;
            return t switch
            {
                <= 5 => new Camera(
                    new Vector(0, 8 - 1.4 * t, 0), 
                    new Vector(0, 0, 0), 
                    new Vector(0, 0, -1),
                    aspectRatio, 
                    Math.PI / 2),
                > 5 and <= 7 => new Camera(
                    new Vector(0, 8 - 1.4 * 5, 0 + 2 * (t - 5) / (7 - 5)), 
                    new Vector(0, 0, 0),
                    new Vector(0, 0, -1), 
                    aspectRatio, 
                    Math.PI / 2),
                _ => new Camera(
                    new Vector(Math.Sin((t - 7) * angFreq), 8 - 1.4 * 5, 2 * Math.Cos((t - 7) * angFreq)),
                    new Vector(0, 0, 0), 
                    new Vector(0, 1, 0), 
                    aspectRatio, 
                    Math.PI / 2)
            };
        };
    }

    public static Scene MakeWorld()
    {
        return new Scene(
            new List<Entity>
            {
                new Sphere(new Vector(0, -100.5, -1), 100,
                    new Diffuse(new Color(0.3, 0.3, 0.3))),

                // Front
                new Sphere(new Vector(0, 0, 1), 0.5,
                    new Diffuse(new Color(0.9, 0.3, 0.3))),

                // Back
                new Sphere(new Vector(0, 0, -1), 0.5,
                    new Metal(new Color(0.1, 0.2, 0.6), 0.4)),

                // Left
                new Sphere(new Vector(-1, 0, 0), 0.5,
                    new Metal(new Color(0.8, 0.8, 0.8), 0.1)),

                // Right
                //new Sphere(new Vector(1, 0, -1), 0.5, 
                //                            new Metal(new Color(0.8, 0.6, 0.2), 0.9)),
                new Sphere(new Vector(1, 0, 0), 0.5,
                    new Glass(1.5))
            });
    }
}