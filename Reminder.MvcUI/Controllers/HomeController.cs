using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reminder.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Reminder.MvcUI.DataAccess.Abstract;
using Reminder = Reminder.MvcUI.Entities.Reminder;

namespace Reminder.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IReminderRepository _reminderRepository;
        public HomeController(ILogger<HomeController> logger, IReminderRepository reminderRepository)
        {
            _logger = logger;
            _reminderRepository = reminderRepository;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.TimeOuts = _reminderRepository.GetTimeOuts();
            model.NotTimeYets = _reminderRepository.GetNotTimeYets();
            model.NowIsTime = _reminderRepository.GetNowIsTime();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreateReminder(ReminderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _reminderRepository.Add(new Entities.Reminder
                {
                    Title = model.Title,
                    ReminderDate = model.ReminderDate
                });
                return RedirectToAction("Index");
            }

            return PartialView("CreateReminderPartialView", model);
        }

        public IActionResult TimeOut(Guid id)
        {
            _reminderRepository.TimeOut(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void Retitle(ReminderRetitleModel model)
        {
            _reminderRepository.Retitle(model.Title, new Entities.Reminder { Id = model.id });
        }
        public List<Entities.Reminder> NowIsTime(Guid id)
        {
            _reminderRepository.NowIsTime(id);

            return _reminderRepository.GetNowIsTime();
        }
        public IActionResult Remove(Guid id)
        {
            _reminderRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
