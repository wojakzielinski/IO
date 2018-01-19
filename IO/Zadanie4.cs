using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Zadanie4
{
    private static Object obiekt = new Object();

    public static void execute()
    {
        ThreadPool.QueueUserWorkItem(Serwer);
        ThreadPool.QueueUserWorkItem(Klient); 
        ThreadPool.QueueUserWorkItem(Klient);
        ThreadPool.QueueUserWorkItem(Klient);
        ThreadPool.QueueUserWorkItem(Klient);
        ThreadPool.QueueUserWorkItem(Klient);
        ThreadPool.QueueUserWorkItem(Klient);
    }

    static void Serwer(Object stateInfo)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 2048);
        server.Start();
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(KlientSerwer, client);
        }
    }

    static void Klient(Object stateInfo)
    {
        TcpClient client = new TcpClient();
        client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
        byte[] message = new ASCIIEncoding().GetBytes("tekst wiadomosci");
        client.GetStream().Write(message, 0, message.Length);
        byte[] buffer = new byte[1024];
        client.GetStream().Read(buffer, 0, 1024);
        ASCIIEncoding AE = new ASCIIEncoding();
        lock (obiekt)
        {
            writeConsoleMessage("Klient:" + AE.GetString(message), ConsoleColor.Green);
        }

    }

    static void KlientSerwer(Object Ob)
    {
        TcpClient client = (TcpClient)Ob;
        byte[] buffer = new byte[1024];
        client.GetStream().Read(buffer, 0, 1024);
        ASCIIEncoding AE = new ASCIIEncoding();
        lock (obiekt)
        {
            writeConsoleMessage("Server:" + AE.GetString(buffer), ConsoleColor.Red);
        }
        client.GetStream().Write(buffer, 0, buffer.Length);
        client.Close();
    }

    static void writeConsoleMessage(string message, ConsoleColor color)
    {

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
    }  
}
