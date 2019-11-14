using DiscordSkypeFake.Contracts;
using System;
using System.ServiceModel;

namespace DiscordSkypeFake.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** DiscordSkypeFake Server ***");

            using (var host = new ServiceHost(typeof(DSFServer)))
            {
                var tcp = new NetTcpBinding();
                tcp.MaxReceivedMessageSize = int.MaxValue;
                host.AddServiceEndpoint(typeof(IServer), tcp, "net.tcp://localhost:1");

                host.Open();
                Console.WriteLine("Server gestartet");
                Console.ReadLine();
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
