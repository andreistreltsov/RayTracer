namespace RTIOW;

public readonly struct Vector
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;

    private static Random random = new Random();

    public Vector(double x, double y, double z) => (X, Y, Z) = (x, y, z);

    public static Vector Zero()
    {
        return new Vector(0, 0, 0);
    }

    public static Vector operator -(Vector v)
    {
        return new Vector(-v.X, -v.Y, -v.Z);
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }
    
    public static Vector operator *(Vector v, double t)
    {
        return new Vector(v.X * t, v.Y * t, v.Z * t);
    }

    public static Vector operator *(double t, Vector v)
    {
        return v * t;
    }

    public static Vector operator /(Vector v, double t)
    {
        return v * (1 / t);
    }

    public static Vector Random()
    {
        return new Vector(random.NextDouble() * 2 - 1,
            random.NextDouble() * 2 - 1,
            random.NextDouble() * 2 - 1);
    }

    public double Dot(Vector v)
    {
        return X * v.X + Y * v.Y + Z * v.Z;
    }

    public bool SameDirection(Vector other)
    {
        return Direction().Dot(other.Direction()) > 0;
    }

    public Vector Cross(Vector v)
    {
        return new Vector(Y * v.Z - Z * v.Y,
                          Z * v.X - X * v.Z,
                          X * v.Y - Y * v.X);
    }

    public Vector Direction()
    {
        return this / Length;
    }

    public double LengthSquared => X * X + Y * Y + Z * Z;

    public double Length => Math.Sqrt(LengthSquared);

    public static Vector RandomPointInUnitSphere()
    {
        while (true)
        {
            var r = Vector.Random();
            if (r.Length < 1)
            {
                return r;
            }
        }
    }

    public static Vector RandomPointOnUnitSphereSurface()
    {
        return RandomPointInUnitSphere().Direction();
    }

}