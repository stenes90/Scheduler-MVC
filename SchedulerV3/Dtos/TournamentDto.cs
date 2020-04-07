using SchedulerV3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchedulerV3.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [StartDateLowerThenEndDateValidation]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}