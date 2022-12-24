using RTIOW;

const int imageWidth = 3840;
const double aspectRatio = 16.0 / 9.0;
const int imageHeight = (int)(imageWidth / aspectRatio);
const int numRowsPerRenderJob = 30;

var origin = new Vector(0, 0, 0);
var camera = new Camera(origin, aspectRatio);
var numSamplesPerPixel = 32;

var world = new World(
    new List<Entity>
    {
        new Sphere(new Vector(0, -100.5, -1), 100, 
            new Diffuse(new Color(0.3, 0.3, 0.3))),
        
        new Sphere(new Vector(0, 0, -1), 0.5, 
                    new Diffuse(new Color(0.7, 0.3, 0.3))),
        
        new Sphere(new Vector(-1, 0, -1), 0.5, 
                            new Metal(new Color(0.8, 0.8, 0.8), 0.1)),
        
        new Sphere(new Vector(1, 0, -1), 0.5, 
                                    new Metal(new Color(0.8, 0.6, 0.2), 0.9)),
    });

var random = new Random();

var image = new Image(imageHeight, imageWidth, numSamplesPerPixel);

IEnumerable<int> Jobs()
{
    var nextJob = imageHeight - 1;
    while (nextJob >= 0)
    {
        yield return nextJob;
        nextJob -= numRowsPerRenderJob;
    }
}

var numJobsLeft = imageHeight / numRowsPerRenderJob + 1;

void RenderRows(int startRow, int numRows)
{
    for (var j = startRow; j >= Math.Max(0, startRow - numRows + 1); j--)
    {
        for (var i = 0; i < imageWidth; i++)
        {
            var color = new Color();

            for (var s = 0; s < numSamplesPerPixel; s++)
            {
                var u = (i + random.NextDouble()) / (imageWidth - 1);
                var v = (j + random.NextDouble()) / (imageHeight - 1);
                color += camera.GetRay(u, v).Color(world, 0);
            }

            // TODO: Most likely a data race here, since different jobs may compute overlapping pixels due to AA.
            image.AddPixel(j, i, color);
        }
    }
}

Parallel.ForEach(Jobs(), startRow =>
{
    RenderRows(startRow, numRowsPerRenderJob);
    numJobsLeft -= 1; // TODO: Data race
    Console.Error.WriteLine($"Jobs remaining: {numJobsLeft}");
});

image.Write();
