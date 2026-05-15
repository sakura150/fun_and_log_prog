
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    struct Triangle
    {
        public long A { get; set; }
        public long B { get; set; }
        public long C { get; set; }
        public long Perimeter { get; set; }
    }


    static long GCD(long a, long b) => b == 0 ? Math.Abs(a) : GCD(b, a % b);

    static void Main(string[] args)
    {
        List<Triangle> result = new List<Triangle>();
        
        long maxPerimeter = 100_000_000;
        long.TryParse(args[0], out long perimiterLimit);
        maxPerimeter = perimiterLimit;


        using (Process currentProcess = Process.GetCurrentProcess())
        {
            currentProcess.Refresh();

            var sw = Stopwatch.StartNew();

            long maxM = (long)Math.Sqrt(maxPerimeter / 2) + 1;

            for (long m = 2; m <= maxM; m++)
            {
                for (long n = 1; n < m; n++)
                {

                    if ((m - n) % 2 == 0 || GCD(m, n) != 1)
                        continue;


                    long a0 = m * m - n * n;
                    long b0 = 2 * m * n;
                    long c0 = m * m + n * n;

                    long diff = Math.Abs(a0 - b0);


                    if (c0 % diff == 0)
                    {

                        long k = 1;
                        while (true)
                        {
                            long a = a0 * k;
                            long b = b0 * k;
                            long c = c0 * k;
                            long perimeter = a + b + c;

                            if (perimeter > maxPerimeter)
                                break;

                            result.Add(new Triangle { A = a, B = b, C = c, Perimeter = perimeter });
                            k++;
                        }
                    }
                }
            }

            sw.Stop();

            long totalMemory = currentProcess.WorkingSet64;

            Console.WriteLine($"Общее потребление памяти: {totalMemory / 1024.0 / 1024.0:F2} МБ");

            Console.WriteLine($"Время выполнения: {sw.Elapsed.TotalMilliseconds} мс");

            Console.WriteLine($"Всего найдено треугольников: {result.Count}");

            
        }
               

    }
}