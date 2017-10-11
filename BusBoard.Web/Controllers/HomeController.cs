using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;
using System.Web.Mvc;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BusInfo(StationID stationID)
        {
            var info = new BusInfo(stationID);
            return View(info);
        }

        public ActionResult ChooseStop(PostcodeSelection selection)
        {
            ValidatePostCode validatePostCode = new ValidatePostCode(selection);
            if (validatePostCode.valid)
            {
                    ChooseStop stop = new ChooseStop(selection);
                    return View(stop);
            }
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Information about this site";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us!";

            return View();
        }
    }
}