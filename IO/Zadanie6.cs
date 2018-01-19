using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IO
{
    class Zadanie6
    {
        public static void execute()
        {
            FileStream fs = new FileStream("C:\\Users\\Szymon\\source\\repos\\plik.txt", FileMode.Open);
            byte[] message = new byte[2048];
            fs.BeginRead(message, 0, message.Length, myAsyncCallback, new object[] { fs, message });
        }

        static void myAsyncCallback(IAsyncResult state)
        {
            var dane = (object[])state.AsyncState;
            byte[] msg = (byte[])dane[1];
            FileStream fs = (FileStream)dane[0];
            String result = System.Text.Encoding.UTF8.GetString(msg);
            Console.WriteLine(result);
            fs.Close();
        }
    }
}



