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
            Mapper.CreateMap<Models.Tournament, TournamentDto>();
            Mapper.CreateMap<TournamentDto, Models.Tournament>();
            Mapper.CreateMap<Models.Court, CourtDto>();
            Mapper.CreateMap<CourtDto, Models.Court>();


        }
    }
}