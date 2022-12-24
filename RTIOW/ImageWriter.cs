namespace RTIOW;

class ImageWriter
{
    const int MaxColor = 255;
    public ImageWriter(double height, double width)
    {
        Console.WriteLine("P3");
        Console.WriteLine($"{width} {height}");
        Console.WriteLine(MaxColor);
    }

    private double Clamp(double x)
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

    public void WritePixel(Color pixel)
    {
        pixel = pixel.GammaCorrect2(); 
        Console.WriteLine($"{(int)((MaxColor+1)*Clamp(pixel.R))} {(int)((MaxColor+1)*Clamp(pixel.G))} {(int)((MaxColor+1) * Clamp(pixel.B))}");
    }
}