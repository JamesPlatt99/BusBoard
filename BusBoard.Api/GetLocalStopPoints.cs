using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.Api
{
    public class GetLocalStopPoints
    {
        public static List<StopPoint> ReturnStopPoints(UserLocation userLocation, string radius)
        {
            RestClient client = new RestClient("https://api.tfl.gov.uk/");
            RestRequest request = new RestRequest("StopPoint", Method.GET);
            Console.Write("Please enter the maximum distance(m): ");
            request.AddParameter("stopTypes", "NaptanPublicBusCoachTram");
            request.AddParameter("lat", userLocation.latitude.ToString());
            request.AddParameter("lon", userLocation.longitude.ToString());
            request.AddParameter("radius", radius);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<LocalStopPointsResponse>(response.Content).StopPoints;
        }

    }
}
