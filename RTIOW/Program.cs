using RTIOW;

const int imageWidth = 800;
const double aspectRatio = 16.0 / 9.0;
const int imageHeight = (int)(imageWidth / aspectRatio);

var origin = new Vector(0, 0, 0);
var camera = new Camera(origin, aspectRatio);
var image = new ImageWriter(imageHeight, imageWidth);
var numSamplesPerPixel = 64;

var world = new World(
    new List<Entity>
    {
        new Sphere(new Vector(0, 0, -1), 0.5),
        new Sphere(new Vector(0, -100.5, -1), 100)
    });

var random = new Random();

for (var j = imageHeight - 1; j >= 0; j--)
{
    for (var i = 0; i < imageWidth; i++)
    {
        var color = new Color();
        
        for (var s = 0; s < numSamplesPerPixel; s++)
        {
            var u = (i + random.NextDouble()) / (imageWidth - 1);
            var v = (j + random.NextDouble()) / (imageHeight - 1);
            color += camera.GetRay(u, v).Color(world);
        }
        
        image.WritePixel(color/numSamplesPerPixel);
    }
}
