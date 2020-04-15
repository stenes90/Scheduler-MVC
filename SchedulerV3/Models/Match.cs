using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulerV3.Models
{
    public class Match
    {
        

        public int Id { get; set; }

        public int Round { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Court Court { get; set; }

        public Class Class { get; set; }

        public Tournament Tournament { get; set; }

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