using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BusBoard.Web.Controllers;
using BusBoard.Web.Models;

namespace ASP
{
    public class Refresher
    {
        public void refresh(StationID stationID)
        {
            return;
            DateTime start = DateTime.Now;
            while (true)
            {
                if (!((DateTime.Now - start).TotalSeconds > 5)) continue;
                HomeController homeController = new HomeController();
                ActionResult  = homeController.BusInfo(stationID);
            }
        }

        public ActionResult ActionResult { get; set; }
    }
}