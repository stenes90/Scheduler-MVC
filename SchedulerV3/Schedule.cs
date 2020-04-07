using SchedulerV3.Models;
using SchedulerV3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Itenso.TimePeriod;

namespace SchedulerV3.Models
{
    public class Schedule
    {
        private ApplicationDbContext _context;
        public Schedule()
        {
            _context = new ApplicationDbContext();
        }


        public List<Match> GenerateMatches(Tournament tournament)
        {
            var classes = _context.Classes
                .Include(z => z.PlayingDates
                .Select(x => x.Courts))
                .Where(c => c.TournamentId == tournament.Id).ToList();

            tournament.Classes = classes;

            var matches = new List<Match>();
            var maxRounds = classes.Select(c => c.NumberOfRounds).ToList().Max();
            var actualRound = 1;
            int i = 0;
            while (maxRounds > 0)
            {
                for (i = 0; i < classes.Count; i++)
                {
                    var actualClass = classes[i];
                    for (int j = 0; j < actualClass.MatchesPerRound; j++)
                    {
                        if (actualRound > actualClass.NumberOfRounds)
                        {
                            continue;
                        }


                        var match = new Match();
                        match.Class = actualClass;
                        match.Round = actualRound;
                        match.IsScheduled = false;
                        match.Tournament = tournament;
                        matches.Add(match);
                    }

                }
                actualRound++;
                maxRounds--;


            }

            tournament.Matches = matches;
            return matches;
        }



        public Tournament ScheduleMatches(Tournament tournament)
        {
            //var tn = _context.Tournaments
            //    .Include(c => c.Classes.Select(x => x.PlayingDates))
            //    .SingleOrDefault(z => z.Id == tournament.Id);

            //var classes = _context.Classes
            //    .Include(z => z.PlayingDates
            //    .Select(x => x.Courts))
            //    .Where(c => c.TournamentId == tournament.Id).ToList();


            var matches = GenerateMatches(tournament);
            int scheduleIndex = 1;
            var listOfScheduledMatches = new List<Match>();
            foreach (var date in tournament.PlayingDates)
            {

                var listOfCourts = date.Courts.ToList();
                var endTime = date.EndTime;
                var classesForDate = date.Classes;
                var matchesForDate = matches.Where(c => c.Class.PlayingDates.Contains(date)).ToList()
                    .Where(x => x.IsScheduled == false).ToList();

                var matchIndex = 0;
                foreach (var match in matches)
                {
                    matchIndex++;
                    var nextAvailableTimeForClassMatch = new DateTime();
                    try
                    {
                        var lastScheduledMatchFromSameClassForPreviousRound =
                        listOfScheduledMatches.Where(c => c.Round == (match.Round - 1))
                        .Where(x => x.Class == (match.Class)).ToList().Last();
                        nextAvailableTimeForClassMatch = lastScheduledMatchFromSameClassForPreviousRound.EndTime.AddMinutes(match.Class.BreakBetweenMatches);
                    }
                    catch (Exception)
                    {
                        var banana = 0;
                    }
                    var listOfStartTimesForEachCourt = new List<DateTime>();
                    foreach (var court in date.Courts)
                    {
                        var checkTime = new TimeRange();
                        checkTime.Start = date.StartTime.AddMinutes(1);
                        checkTime.End = checkTime.Start.AddSeconds(16);

                        var scheduledMatchesForCourt = court.Matches.Where(c => c.IsScheduled).ToList();
                        var listOfMatchTimeRangesForCourt = new List<TimeRange>();
                        var courtName = court.Name;
                        foreach (var item in scheduledMatchesForCourt)
                        {
                            listOfMatchTimeRangesForCourt.Add(item.Timerange());
                        }

                        while (checkTime.End.TimeOfDay < date.EndTime.TimeOfDay)
                        {
                            if (scheduledMatchesForCourt.Count == 0)
                            {
                                match.StartTime = date.StartTime;
                                listOfStartTimesForEachCourt.Add(match.StartTime);
                                break;
                            }
                            else
                            {
                                bool overlap = false;
                                for (int i = 0; i < listOfMatchTimeRangesForCourt.Count; i++)
                                {
                                    bool checkTimeRangeIntersectWithScheduledMatch = listOfMatchTimeRangesForCourt[i].IntersectsWith(checkTime);
                                    if (checkTimeRangeIntersectWithScheduledMatch)
                                    {
                                        overlap = true;
                                        break;
                                    }
                                }


                                if (overlap)
                                {
                                    checkTime.End = checkTime.End.AddMinutes(5);
                                    checkTime.Start = checkTime.Start.AddMinutes(5);
                                }
                                else if (checkTime.Start < nextAvailableTimeForClassMatch)
                                {
                                    checkTime.End = checkTime.End.AddMinutes(5);
                                    checkTime.Start = checkTime.Start.AddMinutes(5);
                                }
                                else
                                {
                                    match.StartTime = checkTime.Start.AddMinutes(-1);
                                    listOfStartTimesForEachCourt.Add(match.StartTime);
                                    break;
                                }
                            }

                        }
                    }

                    var noOfscheduledMatches = matches.Where(c => c.IsScheduled == true).ToList().Count;
                    var smallestDate = listOfStartTimesForEachCourt.Min();
                    var indexOfSmallest = listOfStartTimesForEachCourt.IndexOf(smallestDate);
                    match.Court = listOfCourts[indexOfSmallest];
                    listOfCourts[indexOfSmallest].Matches.Add(match);
                    match.PlayingDate = date;
                    date.Matches.Add(match);
                    match.StartTime = smallestDate;
                    match.EndTime = match.StartTime.AddMinutes(match.Class.MatchDuration);
                    match.IsScheduled = true;
                    match.Tournament = tournament;
                    listOfScheduledMatches.Add(match);

                }
            }
            return tournament;
        }


        public List<Match> BreakTimeRanges(Tournament tournament)
        {
            var playingDates = tournament.PlayingDates;
            var courts = tournament.Courts;
            var matches = tournament.Matches;
            var listOfBreakTimeRanges = new List<TimeRange>();

            foreach (var date in playingDates)
            {
                var courtsForDate = courts.Where(c => c.PlayingDates.Contains(date)).ToList();
                foreach (var court in courtsForDate)
                {
                    var checkTime = new TimeRange();
                    checkTime.Start = date.StartTime.AddMinutes(1);
                    checkTime.End = checkTime.Start.AddSeconds(16);

                    var matchesForCourt = matches.Where(c => c.PlayingDate == date).Where(x => x.Court == court).ToList();
                    var matchesRangesForCourt = new List<TimeRange>();
                    foreach (var item in matchesForCourt)
                    {
                        matchesRangesForCourt.Add(item.Timerange());
                    }


                    while (checkTime.End.TimeOfDay < date.EndTime.TimeOfDay)
                    {

                        bool overlap = false;
                        for (int i = 0; i < matchesRangesForCourt.Count; i++)
                        {
                            bool checkTimeRangeIntersectWithScheduledMatch = matchesRangesForCourt[i].IntersectsWith(checkTime);
                            if (checkTimeRangeIntersectWithScheduledMatch)
                            {
                                overlap = true;
                                break;
                            }
                        }
                        if (overlap)
                        {
                            checkTime.End = checkTime.End.AddMinutes(5);
                            checkTime.Start = checkTime.Start.AddMinutes(5);
                        }

                        else if (!overlap)
                        {
                            var BreakTime = new Match();
                            BreakTime.PlayingDate = date;
                            BreakTime.Court = court;
                            BreakTime.StartTime = checkTime.Start;
                            BreakTime.EndTime = BreakTime.StartTime.AddMinutes(5);
                            bool breakTimeOverlap = false;
                            while (BreakTime.EndTime.TimeOfDay < date.EndTime.TimeOfDay)
                            {
                                BreakTime.EndTime = BreakTime.EndTime.AddMinutes(5);
                                if (BreakTime.EndTime.TimeOfDay > date.EndTime.TimeOfDay)
                                {
                                    BreakTime.StartTime = BreakTime.StartTime.AddMinutes(-1);
                                    BreakTime.EndTime = date.EndTime;
                                    matchesRangesForCourt.Add(BreakTime.Timerange());
                                    listOfBreakTimeRanges.Add(BreakTime.Timerange());
                                    matches.Add(BreakTime);
                                    court.Matches.Add(BreakTime);
                                    checkTime.End = date.EndTime;
                                    break;
                                }
                                for (int i = 0; i < matchesRangesForCourt.Count; i++)
                                {
                                    bool breakTimeRangeIntersectWithScheduledMatch = matchesRangesForCourt[i].IntersectsWith(BreakTime.Timerange());
                                    if (breakTimeRangeIntersectWithScheduledMatch)
                                    {
                                        breakTimeOverlap = true;
                                        break;
                                    }
                                }

                                if (breakTimeOverlap == true)
                                {
                                    BreakTime.StartTime = BreakTime.StartTime.AddMinutes(-1);
                                    BreakTime.EndTime = BreakTime.EndTime.AddMinutes(-1);
                                    matchesRangesForCourt.Add(BreakTime.Timerange());
                                    listOfBreakTimeRanges.Add(BreakTime.Timerange());
                                    matches.Add(BreakTime);
                                    court.Matches.Add(BreakTime);
                                    checkTime.End = BreakTime.EndTime.AddSeconds(76);
                                    checkTime.Start = BreakTime.EndTime.AddMinutes(1);
                                    break;
                                }
                            }

                        }
                    }
                }
            }

            return matches.ToList();
        }


    }
}
