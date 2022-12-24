namespace RTIOW;

class Image
{
    private const int MaxColor = 255;
    private readonly Color[,] pixels;
    private readonly int numSamplesPerPixel;
    
    public Image(int height, int width, int numSamplesPerPixel)
    {
        this.numSamplesPerPixel = numSamplesPerPixel;
        pixels = new Color[height, width];
    }

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
    
    private int Height => pixels.GetLength(0);
    private int Width => pixels.GetLength(1);

    public void AddPixel(int row, int col, Color color)
    {
        pixels[row, col] = color;
    }
    
    public void Write()
    {
        Console.WriteLine("P3");
        Console.WriteLine($"{Width} {Height}");
        Console.WriteLine(MaxColor);
        
        for (var j = Height - 1; j >= 0; j--)
        {
            for (var i = 0; i < Width; i++)
            {
                WritePixel(pixels[j,i]/numSamplesPerPixel);
            }
        }
    }

    private static void WritePixel(Color pixel)
    {
        pixel = pixel.GammaCorrect2(); 
        Console.WriteLine($"{(int)((MaxColor+1)*Clamp(pixel.R))} {(int)((MaxColor+1)*Clamp(pixel.G))} {(int)((MaxColor+1) * Clamp(pixel.B))}");
    }
}
