using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IO
{
    class Zadanie5
    {
        static Object objectLock = new Object();
        static int sum = 0, tabSize = 1000;
        public static List<int> listOfInts = new List<int>();
        public static void execute()
        {
            System.Random random = new Random(System.DateTime.Now.Millisecond);
            for (int i = 0; i <= tabSize; i++)
            {
                listOfInts.Add(random.Next(1, 100));
            }
            Console.WriteLine("Type number of threads for table size:" + tabSize);
            int threadsNumber = int.Parse(Console.ReadLine());
            int sliceSize = tabSize / threadsNumber;
            int start = 0;
            int end = sliceSize - 1;
            for (int i = 0; i < threadsNumber; i++)
            {
                ThreadPool.QueueUserWorkItem(Sumator, new object[] { start, end });
                start += sliceSize;
                end += sliceSize;
            }
            Console.WriteLine("\nWynik: " + sum);
        }

        static void Sumator(Object sliceOfTab)
        {
            var start = ((object[])sliceOfTab)[0];
            var end = ((object[])sliceOfTab)[1];
            int tempSum = 0;
            for (int i = (int)start; i <= (int)end; i++)
            {
                tempSum += listOfInts[i];
            }
            Console.WriteLine("\nCzesciowa suma wynosi " + tempSum);
            lock (objectLock)
            {
                sum += tempSum;
            }
        }

    }
}
