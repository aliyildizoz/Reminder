using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Reminder.MvcUI.CustomValidations;

namespace Reminder.MvcUI.Models
{
    public class ReminderCreateViewModel
    {
        public string Title { get; set; }
        [Required]
        [ReminderDate]
        public DateTime ReminderDate { get; set; }
    }
}
