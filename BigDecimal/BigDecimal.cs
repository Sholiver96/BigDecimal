using System;
using System.Numerics;

/// <summary>
/// Decimal represented by a BigInteger a, and an int b as a*10^b.
/// </summary>
public partial struct BigDecimal
{
    public static int Precision = 100;

    private BigInteger Mantissa { get; set; }
    private int Exponent { get; set; }


    public BigDecimal(BigInteger mantissa, int exponent)
    {
        Mantissa = mantissa;
        Exponent = exponent;
        Normalize();
    }

    public override bool Equals(object obj)
    {
        if(obj is BigDecimal other)
        {
            return Exponent == other.Exponent && Mantissa == other.Mantissa;
        }
        return false;
    }

    public override string ToString()
    {
        string s = Mantissa.ToString();
        if (Exponent > 0)
        {
            s += "".PadRight(Exponent, '0');
        }
        else if (Exponent < 0)
        {
            int decimalPos = s.Length + Exponent;
            s = s.Insert(Math.Max(decimalPos,0), decimalPos <= 0 ? "0." + "".PadRight(-decimalPos, '0') : ".");
        }
        return s;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Mantissa, Exponent);
    }
}
