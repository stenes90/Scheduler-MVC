using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulerV3.Dtos
{
    public class Tournament
    {
        public Tournament()
        {
            Courts = new HashSet<Court>();
            //PlayingDates = new HashSet<PlayingDate>();
            Matches = new HashSet<Match>();
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public virtual ICollection<Court> Courts { get; set; }

        //public virtual ICollection<PlayingDate> PlayingDates { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public virtual ICollection<Class> Classes { get; set; }



        //public List<DateTime> AvailableDates()
        //{
        //    var startDate = StartDate;
        //    var endDate = EndDate;
        //    List<DateTime> listOfDates = new List<DateTime>();
        //    for (var date = startDate; date <= endDate; date = date.AddDays(1))
        //        listOfDates.Add(date);

        //    return listOfDates;
        //}





    }
}