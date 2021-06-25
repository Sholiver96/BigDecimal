using System;
using System.Numerics;

public partial struct BigDecimal
{
    public static implicit operator BigDecimal(decimal value)
    {
        var mantissa = (BigInteger)value;
        var exponent = 0;
        decimal scaleFactor = 1.0m;
        while ((decimal)mantissa != value * scaleFactor)
        {
            exponent -= 1;
            scaleFactor *= 10;
            mantissa = (BigInteger)(value * scaleFactor);
        }
        return new BigDecimal(mantissa, exponent);
    }

    public static BigDecimal operator +(BigDecimal value)
    {
        return value;
    }

    public static BigDecimal operator +(BigDecimal left, BigDecimal right)
    {
        Align(ref left, ref right);
        return new BigDecimal(left.Mantissa + right.Mantissa, left.Exponent);
    }

    public static BigDecimal operator -(BigDecimal value)
    {
        value.Mantissa *= -1;
        return value;
    }

    public static BigDecimal operator -(BigDecimal left, BigDecimal right)
    {
        return left + (-right);
    }

    public static BigDecimal operator *(BigDecimal left, BigDecimal right)
    {
        return new BigDecimal(left.Mantissa * right.Mantissa, left.Exponent + right.Exponent);
    }

    public static BigDecimal operator /(BigDecimal dividend, BigDecimal divisor)
    {
        var exponentChange = Precision - (int)Math.Ceiling(BigInteger.Log10(dividend.Mantissa) - BigInteger.Log10(divisor.Mantissa));
        if (exponentChange < 0) exponentChange = 0;

        dividend.Mantissa *= BigInteger.Pow(10, exponentChange);
        return new BigDecimal(dividend.Mantissa / divisor.Mantissa, dividend.Exponent - divisor.Exponent - exponentChange);
    }

    public static bool operator ==(BigDecimal left, BigDecimal right)
    {
        return left.Exponent == right.Exponent && left.Mantissa == right.Mantissa;
    }

    public static bool operator !=(BigDecimal left, BigDecimal right)
    {
        return left.Exponent != right.Exponent || left.Mantissa != right.Mantissa;
    }
}
