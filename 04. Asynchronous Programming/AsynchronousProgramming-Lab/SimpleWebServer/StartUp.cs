namespace SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static void Main()
        {
            var address = IPAddress.Parse("127.0.0.1");
            const int port = 1300;
            var tcpListener = new TcpListener(address, port);
            tcpListener.Start();

            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            var task = Task.Run(async () => await ConnectWithTcpClient(tcpListener));
            task.GetAwaiter().GetResult();
        }

        public static async Task ConnectWithTcpClient(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client Connected.");

                var buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                var message = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(message.Trim());

                var data = Encoding.UTF8.GetBytes($"HTTP/1.1 200 OK{Environment.NewLine}" +
                                                  $"Content-Type: text/plain" +
                                                  $"{Environment.NewLine}" +
                                                  $"{Environment.NewLine}" +
                                                  $"Hello from server!");

                await client.GetStream().WriteAsync(data, 0, data.Length);

                Console.WriteLine("Closing connection.");
                client.GetStream().Dispose();
            }
        }
    }
}
