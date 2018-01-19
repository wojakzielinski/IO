using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class Zadanie13
    {
        public static void execute()
        {
            Zadanie2();
        }

        public static async void Zadanie2()
        {
            bool Z2 = false;
            //ZADANIE 2. ODKOMENTUJ I POPRAW  
            //.NET 4.0
            await Task.Run(() =>
            {
                Z2 = true;
            });

            Console.WriteLine("Z2: "+Z2);

        }

    }
}
