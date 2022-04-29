using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        // steg 1 ladda ner nugget AutoMapper.Extensions.Microsoft.DependencyInjection
        // steg 2 tala om för våran class att det är en automapperfil använd arvet : profile och using Automapper
        // steg 3 generera en costruktor 
        // steg 4 mappa (Map  från => till), function createmap vart skall jag ifrån och vart skall jag 
        // steg 5 injecera i våran programfil
        public AutoMapperProfiles()
        {
            // Map  från => till
            CreateMap<PostVehicleViewModel, Vehicle>(); 
            // .Formember vart ska vi och hur tar vi oss dit
            CreateMap<Vehicle, VehicleViewModel>()
            .ForMember(dest => dest.VehicleId, options => options
            .MapFrom(src => src.Id))
            .ForMember(dest => dest.VehicleName, options => options
            .MapFrom(src => string.Concat(src.Maker, " ", src.Model)));
            
        }
    }
}