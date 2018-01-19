using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO
{
    class Zadanie12
    {
        public static void execute()
        {
            Task t = OperationTask();
            Task.WaitAll(new Task[] { t });
            TResultDataStructure result = ((Task <TResultDataStructure>)t).Result;
            Console.WriteLine("TResultDataStructure: i="+result.I+", j="+result.J);
        }

        public static Task<TResultDataStructure> OperationTask()
        {
            TaskCompletionSource<TResultDataStructure> tcs = new TaskCompletionSource<TResultDataStructure>();
            Task.Run(() =>
            {
                int i = 333;
                int j = 666;
                for(int x = 0; x < 100; x++)
                {
                    i += x * 2;
                    j += x * 3;
                }
                tcs.SetResult(new TResultDataStructure(i, j));
            });
            return tcs.Task;
        }

        public struct TResultDataStructure
        {
            int i, j;
            public int I { get => i; set => i = value; }
            public int J { get => j; set => j = value; }
            public TResultDataStructure(int i, int j) : this()
            {
                I = i;
                J = j;
            }
        }

    }
}
