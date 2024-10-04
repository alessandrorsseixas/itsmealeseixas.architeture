using AutoMapper;
using Its.Me.AleSeixas.Example.Domina.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domain.Tests.ProfileTests
{
    public class ProfileTests
    {

       
        [Fact]
        public void AutoMapper_Personal_Configuration_IsValid()
        {

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<PersonalProfile>());
            IMapper mapper = new AutoMapper.Mapper(configuration);

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_Customer_Configuration_IsValid()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>());
            IMapper mapper = new AutoMapper.Mapper(configuration);

            configuration.AssertConfigurationIsValid();
        }

       

    }
}
