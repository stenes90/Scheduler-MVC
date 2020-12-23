using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchedulerV3.Dtos
{
    public class PlayingDate
    {

       
        
        public int Id { get; set; }

        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }


        public int TournamentId { get; set; }

        


        



    }
}
