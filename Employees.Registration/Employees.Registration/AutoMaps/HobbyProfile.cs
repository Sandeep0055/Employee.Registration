using AutoMapper;
using Data.Abstraction.Models;
using Employees.Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Registration.AutoMaps
{
    public class HobbyProfile : Profile
    {
        public HobbyProfile()
        {
            CreateMap<Hobby, HobbyModel>();

            CreateMap<HobbyModel, Hobby>().ForMember(x => x.Id, options => options.Ignore());

        }
    }
}
