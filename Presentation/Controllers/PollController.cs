using DataAccess.Repositories;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
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
                                    [FromServices] IPollRepository injectedRepo)
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

        [HttpGet]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null) return NotFound();

            return View(poll);
        }

        [HttpPost]
        public IActionResult Vote(int id, int selectedOption, [FromServices] IPollRepository injectedRepo)
        {
            injectedRepo.Vote(id, selectedOption);
            return RedirectToAction("Results", new { id = id });
        }

        public IActionResult Results(int id)
        {
            var poll = _pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null) return NotFound();

            return View(poll);
        }
    }
}
