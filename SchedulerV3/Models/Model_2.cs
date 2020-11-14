using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchedulerV3.Models
{
    public class Match_2
    {
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public string Date { get; set; }


    }
}