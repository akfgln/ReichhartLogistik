using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Models
{
    public static class AutoMapper
    {
        public static IMapper Mapper { get; private set; }
        public static MapperConfiguration MapperConfiguration { get; private set; }
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Employee, EmployeeDTO>();
            });
            MapperConfiguration = config;
            var mapper = new Mapper(config);
            Mapper = mapper;
            return mapper;
        }
    }
}
