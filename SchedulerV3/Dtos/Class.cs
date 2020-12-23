using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchedulerV3.Dtos
{
    public class Class
    {
        public Class()
        {
            PlayingDates = new HashSet<PlayingDate>();
            Matches = new HashSet<Match>();
            MatchDuration = 30;
            BreakBetweenMatches = 30;
        }

        public int Id { get; set; }

        public int TournamentId { get; set; }

        [ForeignKey("TournamentId")]
        public virtual Tournament Tournament { get; set; }

        public string Name { get; set; }

        //public int NumberOfRounds { get; set; }

        //public int MatchesPerRound { get; set; }

        public int MatchDuration { get; set; }

        public int BreakBetweenMatches { get; set; }

        public virtual ICollection<PlayingDate> PlayingDates { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}