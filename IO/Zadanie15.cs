using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO
{
    class Zadanie15
    {
        TcpListener serverListener;
        int port;
        IPAddress ipAddress;
        Task serverTask;
        bool isRunning = false;
        CancellationTokenSource cts = new CancellationTokenSource();

        public Task ServerTask
        {
            get
            {
                return serverTask;
            }
        }
        public IPAddress Address
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (!isRunning) ipAddress = value;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                if (!isRunning) port = value;
            }
        }
        public Zadanie15()
        {
            Address = IPAddress.Any;
            port = 2048;
        }
        public Zadanie15(int port)
        {
            this.port = port;
        }
        public Zadanie15(IPAddress address)
        {
            this.ipAddress = address;
        }

        public static void execute()
        {

            Zadanie15 server = new Zadanie15();
            server.Run();
            Client client1 = new Client();
            Client client2 = new Client();
            client1.Connect();
            client2.Connect();
            CancellationTokenSource ctsClient1 = new CancellationTokenSource();
            CancellationTokenSource ctsClient2 = new CancellationTokenSource();
            var client1T = client1.keepPinging("no działaj 1", ctsClient1.Token);
            var client2T = client2.keepPinging("no działaj 2", ctsClient2.Token);
            ctsClient1.CancelAfter(2222);
            ctsClient2.CancelAfter(4444);
            Console.WriteLine("Client 1:");
            foreach (var msg in client1T.Result)
            {
                Console.WriteLine(msg);
            }

            Console.WriteLine("Client 2:");
            foreach (var msg in client1T.Result)
            {
                Console.WriteLine(msg);
            }

            Task.WaitAll(new Task[] { client1T, client2T });
            server.StopRunning();
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            serverListener = new TcpListener(ipAddress, port);
            serverListener.Start();
            isRunning = true;

            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient client = await serverListener.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                using (cancellationToken.Register(() => client.GetStream().Close()))
                {
                    client.GetStream().ReadAsync(buffer, 0, buffer.Length, cancellationToken).ContinueWith(
                        async (t) =>
                        {
                            int i = t.Result;
                            while (true)
                            {
                                client.GetStream().WriteAsync(buffer, 0, i, cancellationToken);

                                i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length, cancellationToken);

                            }
                        });
                }
            }
        }

        public void RequestCancellation()
        {
            cts.Cancel();
            serverListener.Stop();
        }

        public void Run()
        {
            serverTask = RunAsync(cts.Token);
        }

        public void StopRunning()
        {
            RequestCancellation();
        }

        class Client
        {
            TcpClient client;

            public void Connect()
            {
                client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            }

            public async Task<string> Ping(string message)
            {
                byte[] buffer = new ASCIIEncoding().GetBytes(message);
                client.GetStream().WriteAsync(buffer, 0, buffer.Length);
                buffer = new byte[1024];
                var t = await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, t);
            }

            public async Task<IEnumerable<string>> keepPinging(string message, CancellationToken token)
            {
                List<string> messages = new List<string>();
                bool done = false;
                while (!done)
                {
                    if (token.IsCancellationRequested)
                        done = true;
                    messages.Add(await Ping(message));
                }
                return messages;
            }


        }
    }

}

