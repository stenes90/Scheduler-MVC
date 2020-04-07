using SchedulerV3.Models;
using SchedulerV3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SchedulerV3.Controllers
{
    public class TournamentController : Controller
    {
        private ApplicationDbContext _context;
        public TournamentController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Tournament
        public ActionResult Index()
        {
            var tournaments = _context.Tournaments.ToList();
            //var toto = _context.Tournament
            tournaments.Reverse();
            return View(tournaments);
        }

        public ActionResult Create()
        {
            var viewModal = new TournamentViewModel();
            viewModal.Tournament = new Tournament() { StartDate = DateTime.Today, EndDate = DateTime.Today };
            return View("CreateEditTournament", viewModal);
        }


        public ActionResult Edit(int id)
        {
            var viewModal = new TournamentViewModel();
            viewModal.Tournament = _context.Tournaments.SingleOrDefault(c => c.Id == id);

            return View("CreateEditTournament", viewModal);
        }

        public ActionResult Class(int id)
        {
            var tournament = _context.Tournaments
                .Include(x => x.Classes.Select(z => z.PlayingDates))
                .SingleOrDefault(c => c.Id == id);
            var viewModel = new TournamentViewModel();
            viewModel.Class = new Class();
            viewModel.Tournament = tournament;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddClass(Class @class)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new TournamentViewModel();
                viewModel.Class = @class;
                viewModel.Tournament = _context.Tournaments.SingleOrDefault(x => x.Id == @class.TournamentId);
                return View("Class", viewModel);
            }
            _context.Classes.Add(@class);
            _context.SaveChanges();
            var tournament = _context.Tournaments
                .Include(x => x.PlayingDates)
                .SingleOrDefault(c => c.Id == @class.TournamentId);

            if (tournament.PlayingDates.Count == 0)
            {
                for (int i = 0; i < @class.ListOfPlayingDates.Count; i++)
                {
                    var playingDate = new PlayingDate();
                    playingDate.Date = @class.ListOfPlayingDates[i];
                    playingDate.Classes.Add(@class);
                    playingDate.Tournament = tournament;
                    _context.PlayingDates.Add(playingDate);
                    _context.SaveChanges();
                }


                return RedirectToAction("Class", new { id = @class.TournamentId });
            }

            var listOfTNplayingDates = new List<DateTime>();
            for (int i = 0; i < tournament.PlayingDates.Count; i++)
            {
                listOfTNplayingDates.Add(tournament.PlayingDates[i].Date);
            }
            for (int i = 0; i < @class.ListOfPlayingDates.Count; i++)
            {
                if (listOfTNplayingDates.Contains(@class.ListOfPlayingDates[i]))
                {
                    var index = listOfTNplayingDates.IndexOf(@class.ListOfPlayingDates[i]);
                    @class.PlayingDates.Add(tournament.PlayingDates[index]);
                    tournament.PlayingDates[index].Classes.Add(@class);

                    _context.SaveChanges();
                }
                else
                {
                    var playingDate = new PlayingDate();
                    playingDate.Date = @class.ListOfPlayingDates[i];
                    playingDate.Classes.Add(@class);
                    playingDate.Tournament = tournament;
                    _context.PlayingDates.Add(playingDate);
                    _context.SaveChanges();
                }

            }
            return RedirectToAction("Class", new { id = @class.TournamentId });
        }



        [HttpPost]
        public ActionResult Save(Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditTournament", tournament);
            }
            if (tournament.Id != 0)
            {
                var tournamentInDb = _context.Tournaments.
                    Include(x => x.Classes
                    .Select(z => z.PlayingDates
                    ))
                    .SingleOrDefault(c => c.Id == tournament.Id);
                tournamentInDb.Name = tournament.Name;
                tournamentInDb.StartDate = tournament.StartDate;
                tournamentInDb.EndDate = tournament.EndDate;
            }
            else
            {
                _context.Tournaments.Add(tournament);
            }
            _context.SaveChanges();

            return RedirectToAction("Class", new { id = tournament.Id });
        }


        public ActionResult Court(int id)
        {
            var tournament = _context.Tournaments.SingleOrDefault(c => c.Id == id);
            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tournament;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCourt(TournamentViewModel viewModel)
        {
            var court = viewModel.Court;
            court.Tournament = _context.Tournaments.SingleOrDefault(c => c.Id == viewModel.Tournament.Id);
            _context.Courts.Add(court);
            _context.SaveChanges();
            return RedirectToAction("Court", new { id = viewModel.Tournament.Id });
        }


        public ActionResult Times(int id)
        {
            var tournament = _context.Tournaments
                .Include(c => c.Classes.Select(x => x.PlayingDates))
                .SingleOrDefault(c => c.Id == id);
            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tournament;
            viewModel.PlayingDates = tournament.PlayingDates.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveTimes(Tournament tournament)
        {
            var Ids = tournament.PlayingDates.Select(x => x.Id).ToList();
            var playingDatesInDB = _context.PlayingDates.Where(x => Ids.Contains(x.Id)).ToList();
            for (int i = 0; i < playingDatesInDB.Count(); i++)
            {
                playingDatesInDB[i].StartTime = tournament.PlayingDates.SingleOrDefault(x => x.Id == Ids[i]).StartTime;
                playingDatesInDB[i].EndTime = tournament.PlayingDates.SingleOrDefault(x => x.Id == Ids[i]).EndTime;
                var playingDatesInDBcheck = _context.PlayingDates.Where(x => Ids.Contains(x.Id)).ToList();

                var playingdateCourtsId = tournament.PlayingDates[i].CourtIds;
                var courtsForDate = _context.Courts.Where(c => playingdateCourtsId.Contains(c.Id)).ToList();

                if (courtsForDate.Count > 0)
                {
                    playingDatesInDB[i].Courts.Clear();
                }
                for (int j = 0; j < courtsForDate.Count; j++)
                {
                    playingDatesInDB[i].Courts.Add(courtsForDate[j]);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Times", new { id = tournament.Id });
        }



        public ActionResult ScheduleMatches(Tournament tournament)
        {
            var tn = _context.Tournaments
                .Include(c => c.Classes.Select(x => x.PlayingDates))
                .SingleOrDefault(z => z.Id == tournament.Id);

            //var classes = _context.Classes
            //    .Include(z => z.PlayingDates
            //    .Select(x => x.Courts))
            //    .Where(c => c.TournamentId == tournament.Id).ToList();

            var schedule = new Schedule();
           
            var tnwithMatches = schedule.ScheduleMatches(tn);
            var breaks = schedule.BreakTimeRanges(tn);

            var viewModal = new TournamentViewModel();
            viewModal.Tournament = tnwithMatches;
            viewModal.Classes = tnwithMatches.Classes.ToList();




            return View("Schedule", viewModal);


            // START

            //var tn = _context.Tournaments
            //    .Include(c => c.Classes.Select(x => x.PlayingDates))
            //    .SingleOrDefault(z => z.Id == tournament.Id);

            //var classes = _context.Classes
            //    .Include(z => z.PlayingDates
            //    .Select(x => x.Courts))
            //    .Where(c => c.TournamentId == tournament.Id).ToList();

            ////var matches = new List<Match>();
            //var actualRound = 1;
            //int scheduleIndex = 1;
            //var maxRoundsForSchedule = classes.Select(c => c.NumberOfRounds).ToList().Max();
            //int k = 0;
            //while (maxRoundsForSchedule > 0)
            //{
            //    int i = 0;
            //    var lastCourt = 0;

            //    for (i = 0; i < classes.Count; i++)
            //    {
            //        var actualClass = classes[i];

            //        if (actualRound > actualClass.NumberOfRounds)
            //        {
            //            continue;
            //        }
            //        var matchesForActualRound = actualClass.MatchesPerRound;
            //        var klasaPlayingDates = actualClass.PlayingDates.ToList();
            //        // klasaPlayingDates.Sort((pd1, pd2) => DateTime.Compare(pd1.Date, pd2.Date));
            //        for (int j = 0; j < klasaPlayingDates.Count; j++)
            //        {
            //            if (matchesForActualRound == 0)
            //            {
            //                break;
            //            }
            //            var actualPlayingDate = klasaPlayingDates[j];
            //            var courtsForDate = actualPlayingDate.Courts.ToList();






            //            while (matchesForActualRound != 0)
            //            {
            //                if (matches.Count != 0)
            //                {
            //                    if (k == courtsForDate.Count)
            //                    {
            //                        lastCourt = 0;
            //                    }
            //                    else if (k == 0)
            //                    {
            //                        lastCourt = k + 1;
            //                    }
            //                    else lastCourt = k+1;
            //                }


            //                for (k = lastCourt; k < courtsForDate.Count; k++)
            //                {
            //                    var check = scheduleIndex;

            //                    var match = new Match();
            //                    match.matchScheduleIndex = scheduleIndex;
            //                    var actualCourt = courtsForDate[k];
            //                    var noOfMatchesOnCourt = matches.Where(c => c.Court.Id == actualCourt.Id).ToList().Count;
            //                    if (noOfMatchesOnCourt == 0)
            //                    {
            //                        match.Class = actualClass;
            //                        match.Round = actualRound;
            //                        match.Court = actualCourt;
            //                        match.Date = actualPlayingDate.Date;
            //                        match.StartTime = actualPlayingDate.StartTime;
            //                        match.EndTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                        match.PlayingDate = actualPlayingDate;
            //                        match.Tournament = tn;
            //                        matches.Add(match);
            //                        actualPlayingDate.Matches.Add(match);
            //                        actualCourt.Matches.Add(match);
            //                        scheduleIndex++;

            //                    }
            //                    else
            //                    {
            //                        var lastMatchOnCourt = actualCourt.LastScheduledMatch(matches, actualCourt, actualPlayingDate.Date);
            //                        var lastMatchForDate = actualPlayingDate.LastScheduledMatch(matches, actualPlayingDate.Date);
            //                        //var lastMatchForClassFromPreviousRound = 

            //                        if (lastMatchOnCourt.Class == actualClass && lastMatchOnCourt.Round == actualRound)
            //                        {
            //                            match.Class = actualClass;
            //                            match.Round = actualRound;
            //                            match.Court = actualCourt;
            //                            match.Date = actualPlayingDate.Date;
            //                            match.StartTime = lastMatchOnCourt.EndTime;
            //                            match.EndTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                            match.PlayingDate = actualPlayingDate;
            //                            match.Tournament = tn;
            //                            matches.Add(match);
            //                            actualPlayingDate.Matches.Add(match);
            //                            actualCourt.Matches.Add(match);
            //                            scheduleIndex++;
            //                        }
            //                        else if (lastMatchOnCourt.Class == actualClass && lastMatchOnCourt.Round != actualRound)
            //                        {
            //                            match.Class = actualClass;
            //                            match.Round = actualRound;
            //                            match.Court = actualCourt;
            //                            match.Date = actualPlayingDate.Date;
            //                            match.StartTime = lastMatchForDate.EndTime;
            //                            match.StartTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                            match.EndTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                            match.PlayingDate = actualPlayingDate;
            //                            match.Tournament = tn;
            //                            matches.Add(match);
            //                            actualPlayingDate.Matches.Add(match);
            //                            actualCourt.Matches.Add(match);
            //                            scheduleIndex++;
            //                        }
            //                        else if (lastMatchOnCourt.Class != actualClass && lastMatchOnCourt.Round == actualRound)
            //                        {
            //                            match.Class = actualClass;
            //                            match.Round = actualRound;
            //                            match.Court = actualCourt;
            //                            match.Date = actualPlayingDate.Date;
            //                            match.StartTime = lastMatchForDate.EndTime;
            //                            match.StartTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                            match.EndTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                            match.PlayingDate = actualPlayingDate;
            //                            match.Tournament = tn;
            //                            matches.Add(match);
            //                            actualPlayingDate.Matches.Add(match);
            //                            actualCourt.Matches.Add(match);
            //                            scheduleIndex++;
            //                        }
            //                        else if (lastMatchOnCourt.Class != actualClass && lastMatchOnCourt.Round != actualRound)
            //                        {
            //                            match.Class = actualClass;
            //                            match.Round = actualRound;
            //                            match.Court = actualCourt;
            //                            match.Date = actualPlayingDate.Date;
            //                            match.StartTime = lastMatchOnCourt.EndTime;
            //                            match.EndTime = match.StartTime.AddMinutes(actualClass.MatchDuration);
            //                            match.PlayingDate = actualPlayingDate;
            //                            match.Tournament = tn;
            //                            matches.Add(match);
            //                            actualPlayingDate.Matches.Add(match);
            //                            actualCourt.Matches.Add(match);
            //                            scheduleIndex++;
            //                        }
                                    
            //                    }
            //                    matchesForActualRound--;
            //                    if (matchesForActualRound == 0)
            //                    {
            //                        break;
            //                    }
            //                }

            //            }
            //        }

            //        if (i == classes.Count - 1)
            //        {
            //            //i = 0;
            //            actualRound++;
            //            maxRoundsForSchedule--;
            //            if (maxRoundsForSchedule == 0)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}

            //var viewModal = new TournamentViewModel();
            //viewModal.Tournament = tn;
            //viewModal.Tournament.Matches = matches;
            //viewModal.Classes = classes;
            //return View("Schedule", viewModal);

            //END
        }

        public ActionResult Schedule(int id)
        {
            var tournament = _context.Tournaments
                .Include(c => c.Classes.Select(x => x.PlayingDates))
                .SingleOrDefault(c => c.Id == id);
            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tournament;
            return View(viewModel);
        }



    }
}
