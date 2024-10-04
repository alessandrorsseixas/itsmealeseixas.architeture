using AutoMapper;
using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Domina.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domina.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>()
               .ForMember(dest => dest.Name, opt => opt.Ignore())
               .ReverseMap();
        }
    }
}
