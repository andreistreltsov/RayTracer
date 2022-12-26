namespace RTIOW;

internal class Program
{
    private static void RenderFrame(StreamWriter sw, 
        double t, 
        int imageHeight, 
        int imageWidth, 
        Scene scene, 
        Func<double, Camera> getCameraAtTime)
    {
        var camera = getCameraAtTime(t);
        var random = new Random();
        const int numSamplesPerPixel = 32;
        
        for (var j = imageHeight-1; j >= 0; j--)
        {
            for (var i = 0; i < imageWidth; i++)
            {
                var color = new Color();

                for (var s = 0; s < numSamplesPerPixel; s++)
                {
                    var u = (i + random.NextDouble()) / (imageWidth - 1);
                    var v = (j + random.NextDouble()) / (imageHeight - 1);
                    color += camera.GetRay(u, v).Color(scene);
                }

                Image.WritePixel(sw, color / numSamplesPerPixel);
            }
        }
    }
    
    public static void Main(string[] args)
    {
        const int imageWidth = 1920;
        const double aspectRatio = 16.0 / 9.0;
        const int imageHeight = (int)(imageWidth / aspectRatio);
        
        var (tMin, tMax) = (Setup.TimeStart, Setup.TimeEnd);
        const int fps = Setup.FPS;

        var scene = Setup.MakeWorld();
        var cameraAnimation = Setup.CameraAnimation(aspectRatio);
        var numFramesRemaining = (int)((tMax-tMin) * fps);

        // Without this Parallel.For for some reason creates way too many threads.
        var pOptions = new ParallelOptions()
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        
        const string outputDir = "render_output";

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        Parallel.For(0, (int)((tMax-tMin) * fps), pOptions, (frameNumber) =>
        {
            var filename = Path.Join(outputDir, $"out_{frameNumber:D5}.ppm");
            using var f = File.Open(filename, FileMode.Create);
            using var sw = new StreamWriter(f);
            Image.WriteHeader(sw, imageHeight, imageWidth);
            RenderFrame(sw, tMin + frameNumber * (1.0 / fps), imageHeight, imageWidth, scene, cameraAnimation);

            numFramesRemaining -= 1; // Race condition
            Console.WriteLine($"Frames remaining: {numFramesRemaining}.");
        });
    }
}