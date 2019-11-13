using SelfHost.Contracts;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SelfHost.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF Self Host ***");

            var host = new ServiceHost(typeof(BurgerService));
            host.AddServiceEndpoint(typeof(IBurgerService), new NetTcpBinding(), "net.tcp://localhost:1");
            host.AddServiceEndpoint(typeof(IBurgerService), new BasicHttpBinding(), "http://localhost:2");
            host.AddServiceEndpoint(typeof(IBurgerService), new WSHttpBinding(), "http://localhost:3");
            var ipcBind = new NetNamedPipeBinding();
            ipcBind.Security.Mode = NetNamedPipeSecurityMode.None;
            host.AddServiceEndpoint(typeof(IBurgerService), ipcBind, "net.pipe://burger/");

            var smb = new ServiceMetadataBehavior() { HttpGetEnabled = true, HttpGetUrl = new Uri("http://localhost:2/mex") };
            host.Description.Behaviors.Add(smb);

            host.Open();
            Console.WriteLine("Service läuft");
            Console.ReadKey();
            host.Close();

            Console.WriteLine("Ende");
            Console.ReadLine();

        }
    }
}
