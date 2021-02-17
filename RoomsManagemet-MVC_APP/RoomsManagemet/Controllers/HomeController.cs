using OfferManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OfferManagement.Controllers
{
    [Authorize]
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = string.Empty;

            Bookings bookings = new Bookings();

            return View("IndexBooking", bookings);
        }

        public ActionResult About()
        {
            return View();
        }
}


   
}
