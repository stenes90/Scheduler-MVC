using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchedulerV3.Models
{
    public class StartDateLowerThenEndDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var tournament = (Tournament)validationContext.ObjectInstance;
            if (DateTime.Compare(tournament.EndDate, tournament.StartDate) < 0)
            {
                return new ValidationResult("Start Date should be lower then End Date");
            }
            else return ValidationResult.Success;
        }


    }
}