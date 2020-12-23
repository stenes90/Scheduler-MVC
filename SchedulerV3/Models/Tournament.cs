namespace SchedulerV3.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    public class Tournament
    {
        public Tournament()
        {
            //Classes = new HashSet<Class>();
            Courts = new HashSet<Court>();
            PlayingDates = new List<PlayingDate>();
            Matches = new HashSet<Match>();
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [StartDateLowerThenEndDateValidation]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<Court> Courts { get; set; }

        public virtual List<PlayingDate> PlayingDates { get; set; }

        public virtual ICollection<Match> Matches { get; set; }



        public List<DateTime> AvailableDates()
        {
            var startDate = StartDate;
            var endDate = EndDate;
            List<DateTime> listOfDates = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
                listOfDates.Add(date);

            return listOfDates;
        }


       

    }
}