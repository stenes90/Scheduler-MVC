using SchedulerV3.Models;
using SchedulerV3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using System.Web.Http.Cors;
//using SchedulerV3.RankedinTestDb;

namespace SchedulerV3.Controllers
{
    [AllowCors]
    [EnableCors("https://localhost:44366", "*", "*")]
    public class TournamentController : Controller
    {
        private ApplicationDbContext _context;
        private SchedulerV3.RankedinTestDb.rankedin_testEntities _rinDb;
        public TournamentController()
        {
            _context = new ApplicationDbContext();
            _rinDb = new SchedulerV3.RankedinTestDb.rankedin_testEntities();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            _rinDb.Dispose();
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


        public ActionResult GenerateMatches(Tournament tournament)
        {
            var tn = _context.Tournaments
               .Include(c => c.Classes.Select(x => x.PlayingDates))
               .SingleOrDefault(z => z.Id == tournament.Id);

            var _generator = new MatchGenerator();
            var matches = _generator.GenerateMatches(tn);
            foreach (var item in matches.ToList())
            {

                tn.Matches.Add(item);
                _context.Matches.Add(item);
            }
            _context.SaveChanges();
            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tn;
            viewModel.Classes = tn.Classes.ToList();
            var data = Json(new { dataa = viewModel });
            return View("Schedule2", viewModel);


        }
        [HttpGet]
        public ActionResult GetTnForSchedule(int id)
        {
            var tn = _context.Tournaments
               .Include(c => c.Classes.Select(x => x.PlayingDates))
               .SingleOrDefault(z => z.Id == id);

            var tournament = new SchedulerV3.Dtos.Tournament();
            foreach (var item in tn.Courts)
            {
                var court = new SchedulerV3.Dtos.Court();
                court.Id = item.Id;
                court.TournamentId = tn.Id;
                court.Name = item.Name;
                tournament.Courts.Add(court);
            }
            foreach (var item in tn.Classes)
            {
                var @class = new SchedulerV3.Dtos.Class();
                @class.Id = item.Id;
                @class.TournamentId = tn.Id;
                @class.Name = item.Name;
                foreach (var plDate in item.PlayingDates)
                {
                    var plDat = new Dtos.PlayingDate();
                    plDat.Id = plDate.Id;
                    plDat.Date = plDate.Date.ToString("yyyy-MM-ddTHH:mm:ss");
                    plDat.StartTime = plDate.StartTime.ToString("yyyy-MM-ddTHH:mm:ss");
                    plDat.EndTime = plDate.EndTime.ToString("yyyy-MM-ddTHH:mm:ss");
                    plDat.TournamentId = tn.Id;
                    @class.PlayingDates.Add(plDat);
                }
                tournament.Classes.Add(@class);
            }
            var _generator = new MatchGenerator();
            var matches = _generator.GenerateMatches2(tn);
            foreach (var item in matches)
            {
                var match = new SchedulerV3.Dtos.Match();
                match.Id = item.Id;
                match.ClassId = (int)item.ClassId;
                match.TournamentId = tn.Id;
                match.Round = item.Round;
                tournament.Matches.Add(match);
            }


            


            
            tournament.StartDate = tn.StartDate.ToString("yyyy-MM-ddTHH:mm:ss");
            tournament.EndDate = tn.EndDate.ToString("yyyy-MM-ddTHH:mm:ss");
            //tournament = new Dtos.Tournament();
            var check = Json(new { data = tournament }, JsonRequestBehavior.AllowGet);
            return check;
            //var json = JsonConvert.SerializeObject(tournament);
            //return Json(json, JsonRequestBehavior.AllowGet);
            //return Json(new { data = tournament });
            //return View("Schedule2", viewModel);


        }

        [HttpGet]
        public ActionResult GetMatches(JsonResult matches)
        {
            var tn = _context.Tournaments
                .Include(c => c.Classes)
                .SingleOrDefault(z => z.Id == 1038);

            

            var json = JsonConvert.SerializeObject(tn);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        [DisableCors]
        [HttpGet]
        public void apicheck(Tournament tn)
        {
            var check = 0;
        }


        public ActionResult ScheduleMatches(Tournament tournament)
        {
            //var tn = _context.Tournaments
            //    .Include(c => c.Classes.Select(x => x.PlayingDates))
            //    .SingleOrDefault(z => z.Id == tournament.Id);

            var tn = _context.Tournaments.Include(c => c.Classes).SingleOrDefault(z => z.Id == tournament.Id);

            var schedule = new Schedule();

            var tnwithMatches = schedule.ScheduleMatches(tn);
            //start


            //foreach (var item in tnwithMatches.Matches)
            //{
            //    var match = new Match();
            //    ////match.StartTime = new DateTime(item.StartTime.Year, item.StartTime.Month, item.StartTime.Day, item.StartTime.Hour, item.StartTime.Minute, item.StartTime.Second);
            //    //match.Class = item.Class;
            //    //match.Court = item.Court;
            //    //match.StartTime = item.StartTime;
            //    ////match.StartTime = item.StartTime.ToString("MMMM dd");
            //    ////match.StartTime = DateTime.Now;
            //    //match.MatchDuration = item.MatchDuration;

            //    _context.Matches.Add(match);
            //}
            //_context.SaveChanges();
            //var matchesInDb = _context.Matches.ToList();


            // end
            var viewModal = new TournamentViewModel();
            viewModal.Tournament = tnwithMatches;
            viewModal.Classes = tnwithMatches.Classes.ToList();

            return View("Schedule2", viewModal);
        }

        //[HttpPost]
        //public ActionResult SaveScheduleInDB(Tournament tournament)
        //{

        //    var matchesInDb = _context.Matches.Where(c => c.TournamentId == tournament.Id).ToList();
        //    foreach (var item in matchesInDb)
        //    {
        //        var match = tournament.Matches.SingleOrDefault(c => c.Id == item.Id);
        //        item.StartTime = match.StartTime;
        //        item.EndTime = match.EndTime;
        //        item.PlayingDateId = match.PlayingDateId;
        //        item.CourtId = match.CourtId;
        //        item.MatchDuration = match.MatchDuration;
        //        item.IsScheduled = true;


        //    }
        //    _context.SaveChanges();
        //    var tn = _context.Tournaments
        //        .Include(c => c.Classes.Select(x => x.PlayingDates))
        //        .SingleOrDefault(c => c.Id == tournament.Id);

        //    var viewModel = new TournamentViewModel();
        //    viewModel.Tournament = tn;
        //    viewModel.Classes = tn.Classes.ToList();
        //    return View("Schedule2", viewModel);
        //}

        [HttpPost]
        public ActionResult SaveScheduleInDB(List<Match> matches)
        {

            var tournamentId = matches[0].TournamentId;
            var matchesInDb = _context.Matches.Where(c => c.TournamentId == tournamentId).ToList();
            foreach (var item in matchesInDb)
            {
                var match = matches.SingleOrDefault(c => c.Id == item.Id);
                item.StartTime = match.StartTime;
                item.EndTime = match.EndTime;
                item.PlayingDateId = match.PlayingDateId;
                item.CourtId = match.CourtId;
                item.MatchDuration = match.MatchDuration;
                item.IsScheduled = true;


            }
            _context.SaveChanges();
            var tn = _context.Tournaments
                .Include(c => c.Classes.Select(x => x.PlayingDates))
                .SingleOrDefault(c => c.Id == tournamentId);

            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tn;
            viewModel.Classes = tn.Classes.ToList();
            return View("Schedule2", viewModel);
        }



        public ActionResult Schedule(int id)
        {
            var tournament = _context.Tournaments
                .Include(c => c.Classes.Select(x => x.PlayingDates))
                .SingleOrDefault(c => c.Id == id);
            var viewModel = new TournamentViewModel();
            viewModel.Tournament = tournament;
            viewModel.Classes = tournament.Classes.ToList();
            return View("Schedule2", viewModel);
        }


        public ActionResult TestDB()
        {

            var check = _rinDb.Tournaments.Take(10).Select(c => new Tournament { Name = c.t_name }).ToList();


            return View(check);
        }


    }
}
