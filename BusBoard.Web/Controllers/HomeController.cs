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
            PostcodeSelection error;
            ValidatePostCode validatePostCode = new ValidatePostCode(selection);
            if (validatePostCode.valid)
            {
                int i = 0;
                if (selection.MaxDistance == null)
                {
                    selection.MaxDistance = "200";
                }
                if(int.TryParse(selection.MaxDistance, out i)) { 
                    ChooseStop stop = new ChooseStop(selection);
                    return View(stop);
                }
                error = new PostcodeSelection { Error = "Invalid max distance." };
                return View("Index", error);
            }
            error = new PostcodeSelection {Error = "Invalid post code"};
            return View("Index", error);
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