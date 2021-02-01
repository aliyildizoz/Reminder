using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reminder.MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Reminder.MvcUI.DataAccess.Abstract;

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

        public HomeIndexViewModel GetModel()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.TimeOuts = _reminderRepository.GetTimeOuts();
            model.NotTimeYets = _reminderRepository.GetNotTimeYets();
            model.NowIsTime = _reminderRepository.GetNowIsTime();
            return model;
        }
        public HomeIndexViewModel GetModel(ReminderCreateViewModel createViewModel)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.TimeOuts = _reminderRepository.GetTimeOuts();
            model.NotTimeYets = _reminderRepository.GetNotTimeYets();
            model.NowIsTime = _reminderRepository.GetNowIsTime();
            model.CreateViewModel = createViewModel;
            return model;
        }
        public IActionResult Index()
        {
            return View(GetModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Index(HomeIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                _reminderRepository.Add(new Entities.Reminder
                {
                    Title = model.CreateViewModel.Title,
                    ReminderDate = model.CreateViewModel.ReminderDate
                });
            }
            else
            {
                ViewBag.Error= string.Join("; ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));
            }

            var indexViewModel = GetModel(model.CreateViewModel);
            
            return View(indexViewModel);
        }

        public IActionResult TimeOut(Guid id)
        {
            _reminderRepository.TimeOut(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void Retitle(ReminderRetitleModel model)
        {
            _reminderRepository.Retitle(model.Title, new Entities.Reminder { Id = model.Id });
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
