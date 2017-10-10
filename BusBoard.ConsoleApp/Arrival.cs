using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class Arrival
    {
        public string id { get; set; }
        public string vehicleID { get; set; }
        public string direction { get; set; }
        public DateTime expectedArrival { get; set; }
    }
}
