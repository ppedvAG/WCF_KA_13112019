using DiscordSkypeFake.Contracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;

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
                tcp.Security.Mode = SecurityMode.None;
                host.AddServiceEndpoint(typeof(IServer), tcp, "net.tcp://localhost:1");

                var http = new WSDualHttpBinding();
                http.Security.Mode = WSDualHttpSecurityMode.Message;
                http.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
                //http.Security.Message.NegotiateServiceCredential = false;
                http.MaxReceivedMessageSize = int.MaxValue;


                host.AddServiceEndpoint(typeof(IServer), http, "http://localhost:2");

                host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                host.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindByThumbprint, "69ff17bd74b7e71da25dd06e2952276a4b956a36");


                host.Open();
                Console.WriteLine("Server gestartet");
                Console.ReadLine();
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
