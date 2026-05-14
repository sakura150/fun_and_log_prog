using System;
using System.Collections.Generic;
using System.Numerics;

class Euler281Final
{
    static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long t = b;
            b = a % b;
            a = t;
        }
        return a;
    }

    static BigInteger Factorial(long n)
    {
        BigInteger result = 1;
        for (long i = 2; i <= n; i++) result *= i;
        return result;
    }

    static BigInteger F(int m, int n)
    {
        long N = (long)m * n;
        BigInteger sum = 0;

        for (long k = 0; k < N; k++)
        {
            long g = GCD(N, k);
            long cycleLen = N / g;

            if (n % cycleLen != 0) continue;
            long colorCycles = n / cycleLen;
            if (m * colorCycles != g) continue;

            sum += Factorial(g) / BigInteger.Pow(Factorial(colorCycles), m);
        }

        return sum / N;
    }

    static void Main()
    {
        BigInteger limit = BigInteger.Pow(10, 15);
        BigInteger totalSum = 0;

        for (int m = 2; ; m++)
        {
            bool any = false;
            for (int n = 1; ; n++)
            {
                BigInteger val = F(m, n);
                if (val > limit) break;
                any = true;
                totalSum += val;
                Console.WriteLine($"f({m},{n}) = {val}");
            }
            if (!any) break;
        }

        Console.WriteLine($"\nTotal sum = {totalSum}");
    }
}