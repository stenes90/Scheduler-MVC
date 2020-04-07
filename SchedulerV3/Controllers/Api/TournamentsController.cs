using AutoMapper;
using SchedulerV3.Dtos;
using SchedulerV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchedulerV3.Controllers.Api
{
    public class TournamentsController : ApiController
    {
        private ApplicationDbContext _context;
        public TournamentsController()
        {
            _context = new ApplicationDbContext();

        }

        // GET /api/tournaments
        public IEnumerable<TournamentDto> GetTournaments()
        {
            return _context.Tournaments.ToList().Select(Mapper.Map<Tournament, TournamentDto>);
        }

        [HttpDelete]
        public void DeleteTournament(int id)
        {
            var tournamentInDb = _context.Tournaments.SingleOrDefault(c => c.Id == id);
            if (tournamentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Tournaments.Remove(tournamentInDb);
            _context.SaveChanges();
        }

        
            

    }
}
