using System;
using System.Numerics;

public partial struct BigDecimal
{
    public static BigDecimal Pow(BigDecimal a, int n)
    {
        int maxPow2 = 1;
        for(int i = 1; i <= n; i*=2)
        {
            maxPow2 = i;
        }
        BigDecimal result = 1.0M;
        for(int pow2 = maxPow2; pow2 >= 2; pow2/=2)
        {
            if(n >= pow2)
            {
                result = result * a;
                n -= pow2;
            } 
            result = result * result;
        }
        if (n == 1) result = result * a;
        return result;
    }

    public static BigDecimal Exp(BigDecimal a, int terms = 50)
    {
        BigDecimal result = 1.0M;
        BigDecimal term = 1;
        for(int i = 1; i <= terms; i++)
        {
            term = term * (a / i);
            result = result + term;
        }
        return result;
    }
}
