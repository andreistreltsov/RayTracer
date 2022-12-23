namespace RTIOW;

public readonly struct Color
{
    public readonly double R;
    public readonly double G;
    public readonly double B;
    public Color(double r, double g, double b) => (this.R, this.G, this.B) = (r, g, b);
    
    public static Color operator *(Color c, double t)
    {
        return new Color(c.R * t, c.G * t, c.B * t);
    }
    
    public static Color operator *(double t, Color c)
    {
        return c * t;
    }

    public static Color operator +(Color c1, Color c2)
    {
        return new Color(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
    }
    
    public static Color operator /(Color c, double t)
    {
        return c * (1 / t);
    }
    
}