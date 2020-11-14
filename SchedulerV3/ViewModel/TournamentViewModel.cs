using SchedulerV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulerV3.ViewModel
{
    public class TournamentViewModel
    {
        //public TournamentViewModel()
        //{
        //    PlayingDatesStartHour = new List<DateTime>();
        //    PlayingDatesEndHour = new List<DateTime>();
        //}
        public Tournament Tournament { get; set; }

        public Class Class { get; set; }

        public Court Court { get; set; }

        public List<Class> Classes{ get; set; }

        public List<Court> Courts { get; set; }

        public List<PlayingDate> PlayingDates { get; set; }

        public List<Match> Matches { get; set; }


        //public List<DateTime> PlayingDatesStartHour { get; set; }

        //public List<DateTime> PlayingDatesEndHour { get; set; }





    }
}