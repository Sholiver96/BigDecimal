using System;
using System.Numerics;

namespace BigDecimal
{
    /// <summary>
    /// Decimal represented by a BigInteger a, and an int b as a*10^b.
    /// </summary>
    public struct BigDecimal
    {
        public static int Precision = 50;

        private BigInteger Mantissa { get; set; }
        private int Exponent { get; set; }


        public BigDecimal(BigInteger mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;
            Normalize();
        }

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
                left.Exponent = right.Exponent;
                left.Mantissa = left.Mantissa * BigInteger.Pow(10, left.Exponent - right.Exponent);
            }
            else if (left.Exponent < right.Exponent)
            {
                right.Exponent = left.Exponent;
                right.Mantissa = right.Mantissa * BigInteger.Pow(10, right.Exponent - left.Exponent);
            }
        }

        public static implicit operator BigDecimal(decimal value)
        {
            var mantissa = (BigInteger)value;
            var exponent = 0;
            decimal scaleFactor = 1.0m;
            while((decimal)mantissa != value * scaleFactor)
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

        public override string ToString()
        {
            string s = Mantissa.ToString();
            if (Exponent > 0)
            {
                s = s.PadRight(Exponent, '0');
            }
            else if (Exponent < 0)
            {
                int decimalPos = s.Length - Exponent;
                s = s.Insert(decimalPos, decimalPos <= 0 ? "0." + "".PadRight(-decimalPos, '0') : ".");
            }
            return s;
        }
    }
}
