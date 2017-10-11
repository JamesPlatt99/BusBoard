using System.Collections.Generic;
using BusBoard.Api;
using BusBoard.Web.Models;

namespace BusBoard.Web.ViewModels
{
    public class ChooseStop
    {
        public ChooseStop(PostcodeSelection selection)
        {
            StopPoints = GetLocalStops(selection.Postcode, selection.MaxDistance);
            PostCode = selection.Postcode;
        }

        public List<StopPoint> StopPoints { get; set; }
        public string PostCode { get; set; }

        private List<StopPoint> GetLocalStops(string postCode, int maxDistance)
        {
            UserLocation userLocation = GetUserLocation.ReturnUserLocation(postCode);
            return GetLocalStopPoints.ReturnStopPoints(userLocation, maxDistance);
        }
    }
}