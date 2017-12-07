using System;

public class Zadanie1
{

    static void execute()
    {
        ThreadPool.QueueUserWorkItem(Serwer);
        ThreadPool.QueueUserWorkItem(Klient); //???
        ThreadPool.QueueUserWorkItem(Klient); //???
        Thread.Sleep(1000);
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
        byte[] message = new ASCIIEncoding().GetBytes("wiadomosc");
        client.GetStream().Write(message, 0, message.Length);
        byte[] buffer = new byte[1024];
        client.GetStream().Read(buffer, 0, 1024);
        ASCIIEncoding AE = new ASCIIEncoding();
        writeConsoleMessage(AE.GetString(message), ConsoleColor.Green);
        //Console.WriteLine(AE.GetString(buffer)); // !!!

    }
    static void KlientSerwer(Object Ob)
    {
        TcpClient client = (TcpClient)Ob;
        byte[] buffer = new byte[1024];
        client.GetStream().Read(buffer, 0, 1024);
        ASCIIEncoding AE = new ASCIIEncoding();
        writeConsoleMessage(AE.GetString(buffer), ConsoleColor.Red);
        client.GetStream().Write(buffer, 0, buffer.Length);
        //Console.WriteLine(AE.GetString(buffer)); // !!!
        client.Close();
    }

    static void writeConsoleMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
