using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulerV3.Dtos
{
    public class TournamentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}