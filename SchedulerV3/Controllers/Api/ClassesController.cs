using SchedulerV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace SchedulerV3.Controllers.Api
{
    public class ClassesController : ApiController
    {
        private ApplicationDbContext _context;
        public ClassesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /Api/Classes
        [HttpGet]
        public IEnumerable<Class> GetClasses()//int id)
        {
            return _context.Classes.ToList();
            //return _context.Class.Where(c =>c.TournamentId == id).ToList();
        }

        // DELETE /api/Classes/1

        [HttpDelete]
        public void DeleteClass(int Id)
        {
            var @class = _context.Classes.Include(c =>c.PlayingDates).SingleOrDefault(x => x.Id == Id);
            var playingDatesForClass = @class.PlayingDates.ToList();
            if (@class == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); 
            }
            //var tournament = _context.Tournament.SingleOrDefault(c => c.Id == @class.TournamentId);
            //var playingDates = tournament.Classes.Select(c => c.PlayingDates).ToList();
            //var check = playingDates;

            var tournamentInDb = _context.Tournaments
               .Include(x => x.Classes.Select(z => z.PlayingDates))
               .SingleOrDefault(c => c.Id == @class.TournamentId);
            

            var classesInDb = _context.Classes.Include(c => c.PlayingDates).Where(z => z.TournamentId == @class.TournamentId).ToList();
            var playingDates = classesInDb.Select(c => c.PlayingDates).ToList();
            var allPlayingDates = new List<PlayingDate>();
            foreach (var DatesList in playingDates)
            {
                allPlayingDates.AddRange(DatesList);
            }


            var PlayingDatesCount = @class.PlayingDates.Count;
            for (int i = 0; i < PlayingDatesCount; i++)
            {
                var count = allPlayingDates.Where(s => s.Date.ToString() == playingDatesForClass[i].Date.ToString()).Count();
                if (count == 1)
                {
                    _context.PlayingDates.Remove(playingDatesForClass[i]);
                    _context.SaveChanges();
                }
            }

            _context.Classes.Remove(@class);
            _context.SaveChanges();
        }

    }
}
