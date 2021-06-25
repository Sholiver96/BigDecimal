using System.Numerics;

public partial struct BigDecimal
{
    private void Normalize()
    {
        if (Mantissa.IsZero)
        {
            Exponent = 0;
            return;
        }
        BigInteger remainder;
        BigInteger shortened = BigInteger.DivRem(Mantissa, 10, out remainder);
        while (remainder == 0)
        {
            Mantissa = shortened;
            Exponent++;
            shortened = BigInteger.DivRem(Mantissa, 10, out remainder);
        }
    }

    private static void Align(ref BigDecimal left, ref BigDecimal right)
    {
        if (left.Exponent > right.Exponent)
        {
            left.Mantissa = left.Mantissa * BigInteger.Pow(10, left.Exponent - right.Exponent);
            left.Exponent = right.Exponent;
        }
        else if (left.Exponent < right.Exponent)
        {
            right.Mantissa = right.Mantissa * BigInteger.Pow(10, right.Exponent - left.Exponent);
            right.Exponent = left.Exponent;
        }
    }
}
