using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchedulerV3.Dtos
{
    public class Court
    {

        public Court()
        {
            Matches = new HashSet<Match>();
            PlayingDates = new HashSet<PlayingDate>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int TournamentId { get; set; }

        [ForeignKey("TournamentId")]
        public virtual Tournament Tournament { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public virtual ICollection<PlayingDate> PlayingDates { get; set; }




      
    }
}