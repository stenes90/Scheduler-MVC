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
                    playingDate.StartTime = new DateTime(playingDate.Date.Year, playingDate.Date.Month, playingDate.Date.Day, 8, 0, 0);
                    playingDate.EndTime = new DateTime(playingDate.Date.Year, playingDate.Date.Month, playingDate.Date.Day, 22, 0, 0);
                    playingDate.Classes.Add(@class);
                    playingDate.Tournament = tournament;
                    _context.PlayingDates.Add(playingDate);
                    _context.SaveChanges();
                    var playingDatesInDB = _context.PlayingDates.Where(x => x.Tournament.Id == tournament.Id).ToList();
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
                var tnStartTime = tournament.PlayingDates.SingleOrDefault(x => x.Id == Ids[i]).StartTime;
                var tnEndTime = tournament.PlayingDates.SingleOrDefault(x => x.Id == Ids[i]).EndTime;
                playingDatesInDB[i].StartTime = new DateTime(playingDatesInDB[i].StartTime.Year, playingDatesInDB[i]
                    .StartTime.Month, playingDatesInDB[i].StartTime.Day, tnStartTime.Hour, tnStartTime.Minute, tnStartTime.Second);

                playingDatesInDB[i].EndTime = new DateTime(playingDatesInDB[i].EndTime.Year, playingDatesInDB[i]
                    .EndTime.Month, playingDatesInDB[i].EndTime.Day, tnEndTime.Hour, tnEndTime.Minute, tnEndTime.Second);

                //playingDatesInDB[i].StartTime = tournament.PlayingDates.SingleOrDefault(x => x.Id == Ids[i]).StartTime;
                //playingDatesInDB[i].EndTime = tournament.PlayingDates.SingleOrDefault(x => x.Id == Ids[i]).EndTime;

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

            var schedule = new Schedule();

            var tnwithMatches = schedule.ScheduleMatches(tn);

            //foreach (var item in tnwithMatches.Matches)
            //{
            //    var match = new Match();
            //    //match.StartTime = new DateTime(item.StartTime.Year, item.StartTime.Month, item.StartTime.Day, item.StartTime.Hour, item.StartTime.Minute, item.StartTime.Second);
            //    match.Class = item.Class;
            //    match.Court = item.Court;
            //    match.StartTime = item.StartTime;
            //    //match.StartTime = item.StartTime.ToString("MMMM dd");
            //    //match.StartTime = DateTime.Now;
            //    match.MatchDuration = item.MatchDuration;
            //    _context.Matches.Add(match);
            //}
            //_context.SaveChanges();
            //var matchesInDb = _context.Matches.ToList();

            var viewModal = new TournamentViewModel();
            viewModal.Tournament = tnwithMatches;
            viewModal.Classes = tnwithMatches.Classes.ToList();

            return View("Schedule2", tnwithMatches);
        }

        //[HttpPost]
        //public ActionResult SaveScheduleInDB()
        //{

        //}



        public ActionResult Schedule(int id)
        {
            var tournament = _context.Tournaments
                .Include(c => c.Classes.Select(x => x.PlayingDates))
                .SingleOrDefault(c => c.Id == id);
            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tournament;
            viewModel.Classes = tournament.Classes.ToList();
            return View(viewModel);
        }



    }
}
