using AutoMapper;
using Data.Abstraction.Model;
using Employees.Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.Registration.AutoMaps
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>()
                    .ForMember(dest => dest.Qualification,
                    options => options.MapFrom(source => source.Qualification.Name));

            CreateMap<EmployeeModel, Employee>().ForMember(x => x.Id, options => options.Ignore());

        }
    }
}
