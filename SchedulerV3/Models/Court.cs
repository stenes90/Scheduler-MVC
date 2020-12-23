using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SchedulerV3.Models
{
    public class Court
    {
        public Court()
        {
            Matches = new List<Match>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<PlayingDate> PlayingDates { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Tournament Tournament { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<Match> Matches { get; set; }



        

        public Match LastScheduledMatch(List<Match> listOfMatches, Court court, DateTime date)
        {
            var matches = listOfMatches.Where(c => c.Date == date).Where(d =>d.Court.Id == court.Id).ToList();
            return matches.Last();
        }


    }
}