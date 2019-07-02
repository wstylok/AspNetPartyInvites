using Microsoft.AspNetCore.Mvc;
using System;
using PartyInvites.Models;
using System.Linq;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            //greetings and current time info
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 17 ? "Dzień dobry" : "Dobry wieczór";
            ViewBag.Hour = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}";
            ViewBag.Day = $"{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}";

            //days left to party
            int daysLeft = 27 - DateTime.Now.Day;
            string d = daysLeft != 1 ? "dni" : "dzień";
            ViewBag.TimeLeft = daysLeft >= 0 ? $"Do urodzin zostało: {daysLeft} {d}" : "W tym roku już po imprezie, zapraszamy za rok :-)";

            return View("MyView");
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            //TODO: wysyłanie maila do organizatora

            //adding response to Repository
            Repository.AddResponse(guestResponse);
            return View("Thanks", guestResponse);
        }

        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(r => r.WillAttend == true));
        }
    }
}
