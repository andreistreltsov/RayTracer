namespace RTIOW;

public static class Setup
{

    public static World MakeWorld()
    {
        return new World(
            new List<Entity>
            {
                new Sphere(new Vector(0, -100.5, -1), 100,
                    new Diffuse(new Color(0.3, 0.3, 0.3))),

                // Center
                new Sphere(new Vector(0, 0, -1), 0.5,
                    new Diffuse(new Color(0.7, 0.3, 0.3))),

                // Left
                new Sphere(new Vector(-1, 0, -1), 0.5,
                    new Metal(new Color(0.8, 0.8, 0.8), 0.1)),

                // Right
                //new Sphere(new Vector(1, 0, -1), 0.5, 
                //                            new Metal(new Color(0.8, 0.6, 0.2), 0.9)),
                new Sphere(new Vector(1, 0, -1), 0.5,
                    new Glass(1.4))
            });
    }
}