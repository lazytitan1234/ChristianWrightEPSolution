using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        public PollController()
        {
        }

        [HttpGet]
        public IActionResult CreatePoll()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePoll(string title, string option1Text, string option2Text, string option3Text,
                                        PollRepository pollRepository) 
        {
            if (ModelState.IsValid)
            {
                pollRepository.CreatePoll(
                    title,
                    option1Text,
                    option2Text,
                    option3Text,
                    0, 0, 0,
                    DateTime.Now
                );

                return RedirectToAction("PollList");
            }

            return View();
        }
    }
}
