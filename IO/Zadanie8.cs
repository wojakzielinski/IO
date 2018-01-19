using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IO
{
    class Zadanie8
    {
        delegate int DelegateType(object arguments);

        static DelegateType delegateFactorialIt;
        static DelegateType delegateFactorialRec;
        static DelegateType delegateFibonacci;

        public static void execute()
        {
            Console.WriteLine("Synchronicznie:");
            DateTime startTime = DateTime.Now;
            Console.WriteLine("Fibonacci:" + fibonacci(30));
            Console.WriteLine("Silnia iteracyjnie: "+ factorialIt(15));
            Console.WriteLine("Silnia rekurencyjnie:"+factorialRec(15));
            DateTime endTime = DateTime.Now;
            Console.WriteLine("Synchronicznie czas:  " + (endTime - startTime).TotalMilliseconds);
         
            Console.WriteLine("Asynchronicznie:");
            startTime = DateTime.Now;
            delegateFibonacci = new DelegateType(fibonacci);
            Console.WriteLine("Fibonacci: " + delegateFibonacci.Invoke(30));

            delegateFactorialIt = new DelegateType(factorialIt);
            Console.WriteLine("Silnia iteracyjnie: " + delegateFactorialIt.Invoke(15));

            delegateFactorialRec = new DelegateType(factorialRec);
            Console.WriteLine("Silnia rekurencyjnie: " + delegateFactorialRec.Invoke(15));

            endTime = DateTime.Now;
            Console.WriteLine("Asynchronicznie czas :  " + (endTime - startTime).TotalMilliseconds);
        }
        static int factorialIt(object n)
        {
            int wynik = 1;
            for (int i = 1; i <= (int)n; i++)
            {
                wynik *= i;
            }
            return wynik;
        }
        static int factorialRec(object nObject)
        {
            int n = (int)nObject;
            if (n == 1) return 1;
            return factorialRec(n - 1) * n;
        }
        static int fibonacci(object nObject)
        {
            int n = (int)nObject;
            if (n == 1) return 1;
            if (n == 0) return 0;
            return fibonacci(n - 1) + fibonacci(n - 2);
        }


    }
}
