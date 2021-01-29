using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.MvcUI.Models
{
    public class ReminderCreateViewModel
    {
        public string Title { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
