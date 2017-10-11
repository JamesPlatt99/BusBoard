using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        private static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            UserLocation userLocation = GetUserLocation.ReturnUserLocation();
            List<StopPoint> localStopPoints = GetLocalStopPoints.ReturnStopPoints(userLocation);

            DisplayAvailableStops(localStopPoints);
            string busStop = localStopPoints[GetBusStop(localStopPoints.Count)].naptanId;

            List<Arrival> arrivals = GetArrivals.ReturnArrivals(busStop);
            arrivals = arrivals.OrderBy(n => n.ExpectedArrival).ToList();

            Console.WriteLine();
            DisplayArrivals(arrivals);

            Console.ReadLine();

        }

        private static void DisplayArrivals(List<Arrival> arrivals)
        {
            if (arrivals.Count == 0)
            {
                Console.WriteLine("There are no upcoming busses at this stop.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine(
                $"The next {Math.Min(5, arrivals.Count)} busses are scheduled to arrive at the following times:");
            for (int i = 0; i < Math.Min(5, arrivals.Count); i++)
            {
                Console.WriteLine();
                Console.WriteLine("{0:g}", arrivals[i].ExpectedArrival);
                Console.WriteLine(arrivals[i].VehicleID);
                Console.WriteLine(arrivals[i].Direction);
            }
        }

        private static void DisplayAvailableStops(List<StopPoint> localStopPoints)
        {
            Console.WriteLine("Please choose a stop:");
            for (int i = 0; i < localStopPoints.Count; i++)
            {
                Console.WriteLine("   {0} {1} {2}", i, localStopPoints[i].commonName, localStopPoints[i].naptanId);
            }
        }

        private static int GetBusStop(int numStops)
        {
            int output;
            while (!int.TryParse(Console.ReadLine(), out output) && output > numStops - 1)
            {
            }
            return output;
        }

    }

}
