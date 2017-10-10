namespace EvenNumbersThread
{
    using System.Linq;
    using System.Threading;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var minNumber = numbers[0];
            var maxNumber = numbers[1];

            var evenNumbers = new Thread(() =>
            PrintEvenNumbers(minNumber, maxNumber));
            evenNumbers.Start();
            evenNumbers.Join();

            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int minNumber, int maxNumber)
        {
            for (int i = minNumber; i < maxNumber; i++)
            {
                if (i % 2 == 0) Console.WriteLine(i);
            }
        }
    }
}
