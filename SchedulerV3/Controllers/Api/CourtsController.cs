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
    public class CourtsController : ApiController
    {
        private ApplicationDbContext _context;

        public CourtsController()
        {
            _context = new ApplicationDbContext();
        }


        public IEnumerable<CourtDto> GetCourtsForTournament(int id)
        {
            return _context.Courts.Where(c => c.Tournament.Id == id).ToList().Select(Mapper.Map<Court, CourtDto>);
        }

        [HttpDelete]
        public void DeleteCourt(int id)
        {
            var court = _context.Courts.SingleOrDefault(c => c.Id == id);
            if (court == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Courts.Remove(court);
            _context.SaveChanges();
        }

        [HttpPut]
        public void EditCourtName(int id, CourtDto court)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var courtInDb = _context.Courts.SingleOrDefault(c => c.Id == id);
            if (courtInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            courtInDb = Mapper.Map<CourtDto, Court>(court);
            courtInDb.Name = court.Name;
            _context.SaveChanges();

        }

    }
}
