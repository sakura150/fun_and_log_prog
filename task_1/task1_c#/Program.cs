using System;
using System.Collections.Generic;

class Euler141
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

    static void Main()
    {
        const long limit = 1_000_000_000_000; 
        long sum = 0;
        var found = new HashSet<long>();

        for (long p = 2; ; p++)
        {
            long p2 = p * p;
            if (p2 * p2 > limit) break;

            for (long s = 1; s < p; s++)
            {
                if (GCD(p, s) != 1) continue;

                long sp = s * p;
                long s2 = s * s;

                for (long k = 1; ; k++)
                {
                    long k2 = k * k;
                    long n = k2 * s * p2 * p + k * s2; 
                    if (n >= limit) break;

                    long sqrtN = (long)Math.Sqrt(n);
                    if (sqrtN * sqrtN == n && !found.Contains(n))
                    {
                        found.Add(n);
                        sum += n;
                    }
                }

                for (long k = 1; ; k++)
                {
                    long k2 = k * k;
                    long n = k2 * s2 * p2 + k * sp;
                    if (n >= limit) break;

                    long sqrtN = (long)Math.Sqrt(n);
                    if (sqrtN * sqrtN == n && !found.Contains(n))
                    {
                        found.Add(n);
                        sum += n;
                    }
                }
            }
        }

        Console.WriteLine($"Sum = {sum}");
    }
}