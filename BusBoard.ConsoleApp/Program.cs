using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        private const string AppId = "5f30eddc";
        private const string ApiKey = "f1c6c957988fdacfe019e95f21d72abc";

        private static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Console.Write("Please enter the stop point id: ");
            string busStop = Console.ReadLine();
            List<Arrival> arrivals = GetResponse(busStop);
            arrivals = arrivals.OrderBy(n => n.expectedArrival).ToList();
            Console.WriteLine();
            Console.WriteLine("The next 5 busses are scheduled to arrive at the following times:");
            for(int i = 0;i<5;i++)
            {
                Console.WriteLine("{0:g}",arrivals[i].expectedArrival);
            }
            Console.ReadLine();

        }
        private static List<Arrival> GetResponse(string busStop)
        {
            RestClient client = new RestClient("https://api.tfl.gov.uk/");
            RestRequest request = new RestRequest("StopPoint/{ids}/Arrivals", Method.GET);
            request.AddUrlSegment("app_id", AppId);
            request.AddUrlSegment("app_key", ApiKey);
            request.AddUrlSegment("ids", busStop);
            List<Arrival> arrivals = new List<Arrival>();
            IRestResponse response = new RestResponse();
            response = client.Execute(request);

            arrivals = GetArrival(response.Content);
            
            return arrivals;
        }

        private static List<Arrival> GetArrival(string content)
        {
            List<Arrival> arrivals = JsonConvert.DeserializeObject<List<Arrival>>(content);

            return arrivals;
        }
    }

}
