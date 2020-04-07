using AutoMapper;
using SchedulerV3.Dtos;
using SchedulerV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchedulerV3.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Tournament, TournamentDto>();
            Mapper.CreateMap<TournamentDto, Tournament>();
            Mapper.CreateMap<Court, CourtDto>();
            Mapper.CreateMap<CourtDto, Court>();


        }
    }
}