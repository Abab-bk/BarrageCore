namespace BarrageCore.Models;

internal readonly struct Rotation(float degrees) : IEquatable<Rotation>
{
    internal float Degrees { get; } = NormalizeAngle(degrees);
    
    internal float Radians => (float)(Degrees * Math.PI / 180f);
    
    private static float NormalizeAngle(float degrees)
    {
        degrees %= 360;
        return degrees < 0 ? degrees + 360 : degrees;
    }
    
    public bool Equals(Rotation other) => Degrees.Equals(other.Degrees);
    
    public override bool Equals(object? obj) => obj is Rotation other && Equals(other);
    
    public override int GetHashCode() => Degrees.GetHashCode();
    
    public override string ToString() => $"{Degrees}° ({Radians} rad)";
    
    public static bool operator ==(Rotation left, Rotation right) => left.Equals(right);
    public static bool operator !=(Rotation left, Rotation right) => !left.Equals(right);
    
    public static Rotation operator +(Rotation left, float degrees) => new (left.Degrees + degrees);
    public static Rotation operator +(float degrees, Rotation right) => right + degrees;
    public static Rotation operator -(Rotation left, float degrees) => new (left.Degrees - degrees);
    public static Rotation operator -(float degrees, Rotation right) => new (degrees - right.Degrees);
}