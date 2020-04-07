namespace SchedulerV3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Class
    {
        public Class()
        {
            PlayingDates = new HashSet<PlayingDate>();
            ListOfPlayingDates = new List<DateTime>();
        }

        public int Id { get; set; }

        public int TournamentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Number of rounds")]
        public int NumberOfRounds { get; set; }

        [Required]
        [Display(Name = "Matches per round")]
        public int MatchesPerRound { get; set; }

        [Required]
        [Display(Name = "Match duration")]
        public int MatchDuration { get; set; }

        [Required]
        [Display(Name = "Break between matches")]
        public int BreakBetweenMatches { get; set; }

        public List<DateTime> ListOfPlayingDates { get; set; }

        public virtual ICollection<PlayingDate> PlayingDates { get; set; }

        

    }
}