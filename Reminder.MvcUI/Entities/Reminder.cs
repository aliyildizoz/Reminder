using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.MvcUI.Entities
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public DateTime CreateDate { get;  }

        public TimeSpan TimeRemaining => ReminderDate - DateTime.Now;

        [Required]
        public DateTime ReminderDate { get; set; }
        public string ReminderLongDate { get; set; }
        public ReminderState ReminderState { get; set; }
        public Reminder()
        {
            CreateDate = DateTime.Now;
            ReminderState = ReminderState.NotTimeYet;
            Id = Guid.NewGuid();
        }
    }

    public enum ReminderState
    {
        TimeOut = 1,
        NotTimeYet = 2,
        NowIsTime = 3
    }
}
