using DataAccess;
using DataAccess.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly PollRepository _pollRepository;

        public PollController(PollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();
            return View(polls);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string option1Text, string option2Text, string option3Text,
                                    [FromServices] PollRepository injectedRepo)
        {
            injectedRepo.CreatePoll(
                title,
                option1Text,
                option2Text,
                option3Text,
                0, 0, 0,
                DateTime.Now
            );

            TempData["message"] = "Poll created successfully!";
            return RedirectToAction("Index");
        }
    }
}
