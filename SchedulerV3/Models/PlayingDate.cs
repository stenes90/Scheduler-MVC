namespace SchedulerV3.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    public class PlayingDate
    {
        public PlayingDate()
        {
            Courts = new HashSet<Court>();
            //StartTime = new DateTime(2000, 1, 1, 8, 0, 0);
            //EndTime = new DateTime(2000, 1, 1, 22, 0, 0);
            Classes = new HashSet<Class>();
            Matches = new List<Match>();
            CourtIds = new List<int>();

        }
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<int> CourtIds { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Tournament Tournament { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Court> Courts { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Class> Classes { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Match> Matches { get; set; }




        public Match LastScheduledMatch(List<Match> listOfMatches, DateTime date)
        {
            var matches = listOfMatches.Where(c =>c.Date == date).ToList();
            return matches.Last();
        }

        public Match lastMatchForClassFromPreviousRound(List<Match> listOfMatches, int actualRound)
        {
            var matches = listOfMatches.Where(c => c.Round == (actualRound-1)).ToList();
            return matches.Last();
        }



    }
}