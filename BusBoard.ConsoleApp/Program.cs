using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

        private static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            UserLocation userLocation = GetUserLocation();
            List<StopPoint>localStopPoints = GetLocalStopPoints(userLocation);
            Console.WriteLine("Please choose a stop:");
            for (int i = 0; i < localStopPoints.Count; i++)
            {
                Console.WriteLine("   {0} {1}",i,localStopPoints[i].commonName);
            }
            string busStop = localStopPoints[GetBusStop(localStopPoints.Count)].naptanId;
            List<Arrival> arrivals = GetArrivals(busStop);
            arrivals = arrivals.OrderBy(n => n.ExpectedArrival).ToList();
            Console.WriteLine();
            Console.WriteLine("The next 5 busses are scheduled to arrive at the following times:");
            for(int i = 0;i<5;i++)
            {
                Console.WriteLine();
                Console.WriteLine("{0:g}",arrivals[i].ExpectedArrival);
                Console.WriteLine(arrivals[i].VehicleID);
                Console.WriteLine(arrivals[i].Direction);
            }
            Console.ReadLine();

        }

        private static int GetBusStop(int numStops)
        {
            int output;
            while (!int.TryParse(Console.ReadLine(), out output) && output > numStops-1)
            {}
            return output;
        }

        private static List<StopPoint> GetLocalStopPoints(UserLocation userLocation)
        {
            RestClient client = new RestClient("https://api.tfl.gov.uk/");
            RestRequest request = new RestRequest("StopPoint", Method.GET);
            Console.Write("Please enter the maximum distance(m): ");
            request.AddParameter("stopTypes", "NaptanPublicBusCoachTram");
            request.AddParameter("lat", userLocation.latitude.ToString());
            request.AddParameter("lon", userLocation.longitude.ToString());
            request.AddParameter("radius", GetUserMaxDistanceFromStation().ToString());
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<LocalStopPointsResponse>(response.Content).StopPoints;
        }

        private static UserLocation GetUserLocation()
        {
            Console.Write("Please enter your postcode: ");
            string postCode = Console.ReadLine();
            RestClient client = new RestClient("http://api.postcodes.io/postcodes/");
            RestRequest request = new RestRequest(postCode, Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine();
            PostCodeResponse responseObject = JsonConvert.DeserializeObject<PostCodeResponse>(response.Content);
            return responseObject.result;
        }

        private static List<Arrival> GetArrivals(string busStop)
        {
            RestClient client = new RestClient("https://api.tfl.gov.uk/");
            RestRequest request = new RestRequest("StopPoint/{ids}/Arrivals", Method.GET);
            request.AddParameter("app_id", AppId);
            request.AddParameter("app_key", ApiKey);
            request.AddUrlSegment("ids", busStop);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Arrival>>(response.Content);
        }

        static int GetUserMaxDistanceFromStation()
        {
            int output;
            while (!int.TryParse(Console.ReadLine(), out output))
            {
            }
            return output;
        }
    }

}
