using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.MvcUI.CustomValidations
{
    public class ReminderDateAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Reminder date should be set at least 1 minute later.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var releaseYear = (DateTime)value;
            var timeDiff = releaseYear - DateTime.Now;
            if (timeDiff.TotalMinutes < 0)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
