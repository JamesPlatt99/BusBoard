using System.Collections.Generic;
using System.Linq;
using BusBoard.Api;
using BusBoard.Web.Models;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public BusInfo(StationID stationID)
        {
            Arrivals = sortArrivals(GetArrivals.ReturnArrivals(stationID.naptamID), stationID);
            naptamID = stationID.naptamID;
            CommonName = stationID.commonName;
        }

        public string CommonName { get; set; }
        private List<Arrival> sortArrivals(List<Arrival> arrivals, StationID stationID)
        {
            switch (stationID.sortBy)
            {
                case "Time":
                    arrivals = arrivals.OrderBy(n => n.ExpectedArrival).ToList();
                    break;
                case "Destination":
                    arrivals = arrivals.OrderBy(n => n.DestinationName).ToList();
                    break;
                case "Direction":
                    arrivals = arrivals.OrderBy(n => n.Direction).ToList();
                    break;
                case "VehicleID":
                    arrivals = arrivals.OrderBy(n => n.VehicleID).ToList();
                    break;
            }
            return arrivals;
        }
        public string naptamID { get; set; }
        public List<Arrival> Arrivals { get; set; }
    }
}