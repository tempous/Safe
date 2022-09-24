using System;
using System.Linq;

namespace ConsoleSafe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("N = ");
            var n = int.Parse(Console.ReadLine());

            var end = false;
            var step = 0;

            char[,] safe = new char[n, n];

            Initialize(safe);
            Output(safe);

            if (Check(safe))
            {
                end = true;
                Console.WriteLine("Safe opened successfully!\nGame over!");
            }

            while (!end)
            {
                Console.WriteLine($"---------|Step {++step}|---------");

                var coords = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var x = coords[0] - 1;
                var y = coords[1] - 1;

                safe[x, y] = safe[x, y] == '-' ? '|' : '-';

                for (int i = 0; i < n; i++)
                {
                    safe[x, i] = safe[x, i] == '-' ? '|' : '-';
                    safe[i, y] = safe[i, y] == '-' ? '|' : '-';
                }

                Output(safe);

                if (Check(safe))
                {
                    end = true;
                    Console.WriteLine("Safe opened successfully!\nGame over!");
                }

                //var check = Check(safe);
                //if (check.Item1 == safe.Length || check.Item2 == safe.Length)
                //    end = true;
            }
        }

        private static void Initialize(char[,] safe)
        {
            Random random = new();

            for (int i = 0; i < safe.GetLength(0); i++)
                for (int j = 0; j < safe.GetLength(0); j++)
                    safe[i, j] = random.Next(0, 2) == 0 ? '-' : '|';
        }

        private static void Output(char[,] safe)
        {
            for (int i = 0; i < safe.GetLength(0); i++)
            {
                for (int j = 0; j < safe.GetLength(0); j++)
                    Console.Write(safe[i, j] + "\t");

                Console.WriteLine("\n");
            }
        }

        private static bool Check(char[,] safe, int horCount = 0, int verCount = 0)
        {
            var count = safe.Length;

            for (int i = 0; i < safe.GetLength(0); i++)
            {
                for (int j = 0; j < safe.GetLength(0); j++)
                {
                    if (safe[i, j] == '-')
                        horCount++;
                    else
                        verCount++;
                }
            }

            return horCount == count || verCount == count;
        }

        //private static (int, int) Check(char[,] safe, int horCount = 0, int verCount = 0)
        //{
        //    for (int i = 0; i < safe.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < safe.GetLength(0); j++)
        //        {
        //            if (safe[i, j] == '-')
        //                horCount++;
        //            else
        //                verCount++;
        //        }
        //    }

        //    return (horCount, verCount);
        //}
    }
}
