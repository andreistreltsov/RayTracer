using RTIOW;

const int imageWidth = 800;
const double aspectRatio = 16.0 / 9.0;
const int imageHeight = (int)(imageWidth / aspectRatio);
const int numRowsPerRenderJob = 30;

var origin = new Vector(0, 0, 0);
var camera = new Camera(origin, aspectRatio);
const int numSamplesPerPixel = 32;

var world = Setup.MakeWorld();
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

            image.AddPixel(j, i, color);
        }
    }
}

Parallel.ForEach(Jobs(), startRow =>
{
    RenderRows(startRow, numRowsPerRenderJob);
    // Data race, but we don't care since it's only a progress indicator
    Console.Error.WriteLine($"Jobs remaining: {--numJobsLeft}");
});

image.Write();
