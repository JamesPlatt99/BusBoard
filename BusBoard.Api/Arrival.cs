using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    public class Arrival
    {
        public string VehicleID { get; set; }
        public string Direction { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string DestinationName { get; set; }
    }
}
