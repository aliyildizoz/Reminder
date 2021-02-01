using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.MvcUI.Models
{
    public class HomeIndexViewModel
    {
        public List<Entities.Reminder> TimeOuts { get; set; }
        public List<Entities.Reminder> NotTimeYets{ get; set; }
        public List<Entities.Reminder> NowIsTime{ get; set; }
        public ReminderCreateViewModel CreateViewModel { get; set; }

    }
}
