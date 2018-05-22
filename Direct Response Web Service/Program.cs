using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response_Web_Service
{
    class Program
    {
        public static DirectResponseWebService _server;
        static void Main(string[] args)
        {
            _server = new DirectResponseWebService();
            using(ServiceHost serviceHost = new ServiceHost(_server))
            {
                try
                {
                    serviceHost.Open();
                    Console.WriteLine("Service is running");
                    Console.WriteLine("Press <Enter> to shutdown service");
                    Console.ReadLine();

                    serviceHost.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e.Message);
                    serviceHost.Abort();
                }
            }
        }
    }
}
