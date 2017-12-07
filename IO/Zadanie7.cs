using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IO
{
    class Zadanie7
    {
        public static void execute()
        {
            FileStream fs = new FileStream("C:\\Users\\Szymon\\source\\repos\\plik.txt", FileMode.Open);
            byte[] message = new byte[2048];
            var argument = fs.BeginRead(message, 0, message.Length, null, new object[] { fs, message });
            int lenght = fs.EndRead(argument);
            Console.WriteLine(lenght);
            String result = System.Text.Encoding.UTF8.GetString(message);
            Console.WriteLine(result);
            fs.Close();
        }

    }
}
