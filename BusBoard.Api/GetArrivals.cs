using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace BusBoard.Api
{
    public class GetArrivals
    {
        private const string AppId = "5f30eddc";
        private const string ApiKey = "f1c6c957988fdacfe019e95f21d72abc";

        public static List<Arrival> ReturnArrivals(string busStop)
        {
            RestClient client = new RestClient("https://api.tfl.gov.uk/");
            RestRequest request = new RestRequest("StopPoint/{ids}/Arrivals", Method.GET);
            request.AddParameter("app_id", AppId);
            request.AddParameter("app_key", ApiKey);
            request.AddUrlSegment("ids", busStop);
            IRestResponse response = client.Execute(request);
            return RemovePastArrivals(JsonConvert.DeserializeObject<List<Arrival>>(response.Content));
        }

        private static List<Arrival> RemovePastArrivals(List<Arrival> arrivals)
        {
            List<Arrival> futureArrivals = new List<Arrival>();
            foreach (Arrival arrival in arrivals)
            {
                arrival.ExpectedArrival = arrival.ExpectedArrival.ToLocalTime();
                int result = DateTime.Compare(arrival.ExpectedArrival, DateTime.Now);
                if (result >= 0)
                {
                    futureArrivals.Add(arrival);
                }
            }
            return futureArrivals;
        }
    }
}