using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchedulerV3.Models
{
    public class Match
    {

        //[Required]
        //public int CityId { get; set; }

        //[ForeignKey("CityId")]
        //public City City { get; set; }


        public int Id { get; set; }

        public int Round { get; set; }

        [DataType("datetime2")]
        public DateTime Date { get; set; }

        [DataType("datetime2")]
        public DateTime StartTime { get; set; }

        [DataType("datetime2")]
        public DateTime EndTime { get; set; }

        public int MatchDuration { get; set; }

        public int? CourtId { get; set; }

        [ForeignKey("CourtId")]
        public Court Court { get; set; }

        public int? ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        public int? TournamentId { get; set; }

        [ForeignKey("TournamentId")]
        public Tournament Tournament { get; set; }

        public int? PlayingDateId { get; set; }

        [ForeignKey("PlayingDateId")]
        public virtual PlayingDate PlayingDate { get; set; }

        public int matchScheduleIndex { get; set; }

        public bool IsScheduled { get; set; }

        public TimeRange Timerange()
        {
            var timeRange = new TimeRange();
            timeRange.Start = StartTime;
            timeRange.End = EndTime;
            

            return timeRange;
        }

        


    }
}