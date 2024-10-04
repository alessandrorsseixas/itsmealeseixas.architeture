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
    public class PersonalProfile : Profile
    {
        public PersonalProfile()
        {
            CreateMap<Personal, PersonalViewModel>().ReverseMap();
        }

    }
}
