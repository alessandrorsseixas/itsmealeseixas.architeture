using AutoMapper;
using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Domina.Mapper;
using Its.Me.AleSeixas.Example.Domina.VO;
using itsmealeseixas.architeture.utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domain.Tests.Helpers
{
    public static class TestsUtils
    {

       

        public static PersonalViewModel GetPersonalViewModelMockTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<PersonalProfile>());
            IMapper _mapper = new AutoMapper.Mapper(configuration);
            var personalViewModel = _mapper.Map<PersonalViewModel>(GetValidMockPersonal());

            return personalViewModel;
        }

        public static Personal GetValidMockPersonal()
        {

            Personal personal = new Personal()
            {
                Identifier = Guid.Parse("a9ee531f-256c-4c8e-97e5-33e49f260e02"),
                Document = "64558658044",
                Name = "Super Admin",
                DateOfBirth = DateTime.Parse("10/05/1984").ToUniversalTime(),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
             
            };

            return personal;
        }
        public static List<Personal> GetValidMockListPersonal()
        {
            List<Personal> listPersonal = new List<Personal>();
            Personal personal = new Personal()
            {
                Identifier = UtilsHelpers.GenerateId(),
                Document = "64558658044",
                Name = "San Nathsa Wulfwen",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste"
            };

            listPersonal.Add(personal);

            Personal personal1 = new Personal()
            {
                Identifier = UtilsHelpers.GenerateId(),
                Document = "67220086067",
                Name = "Fuiti Uarbi Hyuar",
                DateOfBirth = DateTime.Parse("15/08/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };
            listPersonal.Add(personal1);
            return listPersonal;
        }

        public static DateTime? GetValidDate(string datetime)
        {
            return UtilsHelpers.GetValidDateTime(datetime);

        }

        public static Customer GetCustomerMock()
        {
            var personal = TestsUtils.GetValidMockPersonal();
            Customer customer = new Customer()
            {
                Identifier = UtilsHelpers.GenerateId(),
                Code = UtilsHelpers.GenerateUniqueCustomerCode(),
                Token = UtilsHelpers.GenerateUniqueCustomerToken(),
                Personal = personal,
                IdPersonal = personal.Identifier,
                IsActive = true,
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            return customer;
        }

        



    }
}
