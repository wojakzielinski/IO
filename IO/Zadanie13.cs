using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class Zadanie13
    {
        /*
        static Object lockObject = new Object();
        public static void execute()
        {
            Task server = serverTask();

            Task klient = clientTask();
        }

        static async Task clientTask()
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            byte[] message = new ASCIIEncoding().GetBytes("tekst wiadomosci");
            client.GetStream().Write(message, 0, message.Length);
            byte[] buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, 1024);
            ASCIIEncoding AE = new ASCIIEncoding();
            lock (lockObject)
            {
                writeConsoleMessage("Klient:" + AE.GetString(buffer), ConsoleColor.Green);
            }
        }

        static async Task serverTask()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                client.GetStream().ReadAsync(buffer, 0, buffer.Length).ContinueWith(async (t) =>
                {
                    int i = t.Result;
                    while (true)
                    {
                        client.GetStream().WriteAsync(buffer, 0, i);
                        i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                        ASCIIEncoding AE = new ASCIIEncoding();
                        lock (lockObject)
                        {
                            writeConsoleMessage("Server:" + AE.GetString(buffer), ConsoleColor.Green);
                        }
                    }
                });
            }
        }*/

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
