namespace RTIOW;

static class Image
{
    private const int MaxColor = 255;

    private static double Clamp(double x)
    {
        const double min = 0;
        const double max = 0.999;
        return x switch
        {
            < min => min,
            > max => max,
            _ => x
        };
    }
    
    public static void WriteHeader(StreamWriter writer, int height, int width)
    {
        writer.WriteLine("P3");
        writer.WriteLine($"{width} {height}");
        writer.WriteLine(MaxColor);
    }

    public static void WritePixel(StreamWriter writer, Color color)
    {
        var pixel = color.GammaCorrect2(); 
        writer.WriteLine($"{(int)((MaxColor+1)*Clamp(pixel.R))} {(int)((MaxColor+1)*Clamp(pixel.G))} {(int)((MaxColor+1) * Clamp(pixel.B))}");
    }
}
