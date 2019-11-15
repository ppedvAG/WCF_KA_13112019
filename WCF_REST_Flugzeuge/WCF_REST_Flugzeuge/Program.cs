using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WCF_REST_Flugzeuge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** WCF REST Flugzeuge Service ***");

            using (var host = new ServiceHost(typeof(FlugzeugService)))
            {
                var web = new WebHttpBinding();
                var ep = host.AddServiceEndpoint(typeof(IFlugzeugeService), web, "http://localhost:1");
                ep.EndpointBehaviors.Add(new WebHttpBehavior() { AutomaticFormatSelectionEnabled = true });

                host.Open();
                Console.WriteLine("Service läuft");
                Console.ReadLine();
            }


            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
