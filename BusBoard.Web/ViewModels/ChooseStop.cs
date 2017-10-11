using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class ChooseStop
    {
        public ChooseStop(string postCode)
        {
            List <StopPoint> localStops = GetLocalStops(postCode);
        }
        public string PostCode { get; set; }
        public static List<StopPoint> GetLocalStops(string postCode)
        {
            UserLocation userLocation = GetUserLocation.ReturnUserLocation(postCode);
            return GetLocalStopPoints.ReturnStopPoints(userLocation);
        }
    }
}