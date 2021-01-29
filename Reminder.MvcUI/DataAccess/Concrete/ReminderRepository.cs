using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reminder.MvcUI.DataAccess.Abstract;
using Reminder.MvcUI.Entities;

namespace Reminder.MvcUI.DataAccess.Concrete
{
    public class ReminderRepository : IReminderRepository
    {
        private static List<Entities.Reminder> _reminders;

        public ReminderRepository()
        {
            _reminders = new List<Entities.Reminder>();
        }
        public List<Entities.Reminder> GetNotTimeYets()
        {
            return _reminders.Where(reminder => reminder.ReminderState == ReminderState.NotTimeYet).ToList();
        }

        public List<Entities.Reminder> GetTimeOuts()
        {
            return _reminders.Where(reminder => reminder.ReminderState == ReminderState.TimeOut).ToList();
        }

        public List<Entities.Reminder> GetNowIsTime()
        {
            return _reminders.Where(reminder => reminder.ReminderState == ReminderState.NowIsTime).OrderBy(reminder => reminder.CreateDate).ToList();
        }

        public Entities.Reminder GetById(Guid id)
        {
            var reminder = _reminders.FirstOrDefault(reminder => reminder.Id == id);
            return reminder;
        }

        public void Add(Entities.Reminder reminder)
        {
            reminder.ReminderLongDate = reminder.ReminderDate.ToString("F");
            _reminders.Add(reminder);
        }

        public void Remove(Guid id)
        {
            _reminders.RemoveAll(reminder => reminder.Id == id);
        }

        public void Retitle(string title, Entities.Reminder reminder)
        {
            var result = _reminders.Find(r => r.Id == reminder.Id);
            if (result != null)
            {
                result.Title = title;
            }
        }

        public void TimeOut(Guid id)
        {
            var result = _reminders.Find(r => r.Id == id);
            if (result != null)
            {
                result.ReminderState = ReminderState.TimeOut;
            }
        }
        public void NowIsTime(Guid id)
        {
            var result = _reminders.Find(r => r.Id == id);
            if (result != null)
            {
                result.ReminderState = ReminderState.NowIsTime;
            }

        }

    }
}
