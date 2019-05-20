using System;
using System.Net;
using System.Net.Sockets;

namespace SocketTest
{
    class Program
    {
        static int port = 4558;
        private static readonly Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static Socket client;
        private  readonly static byte[] buf = new byte[sizeof(double)];
        static void Main(string[] args)
        {
            // Bind Server with the EndPoint
            // IPAddress.Any paramiter to Listen on all network interfaces Port is the port
            server.Bind(new IPEndPoint(IPAddress.Any, port));

            // Listen to only one Device
            server.Listen(1);

            // Begin Accept will wait untill a connection comes
            // First parameter is the Action to do when a Connection requested
            // Second parameter is the State it may be used to send some Arguments so they can be retreived when connection requested
            server.BeginAccept((IAsyncResult result) =>
            {
                // Accept request and get Client Socket
                client = server.EndAccept(result);
                // Execute ReceiveData when receiving sizeof(double) Data into buf starting from Zero Index
                client.BeginReceive(buf, 0, sizeof(double), SocketFlags.None, F, null);
            }, null);
            Console.WriteLine("Hello World!");
        }

        static void F(IAsyncResult newr) {
            client.EndAccept(newr);
            double yourResult = BitConverter.ToDouble(buf, 0);
            // yourResult is the received double


            client.BeginReceive(buf, 0, sizeof(double), SocketFlags.None, F, null);
        }
    }
}
