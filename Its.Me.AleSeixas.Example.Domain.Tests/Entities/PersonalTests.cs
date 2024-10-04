using Bogus;
using Bogus.Extensions.Brazil;
using FluentValidation.TestHelper;
using Its.Me.AleSeixas.Example.Domain.Tests.Helpers;
using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Domina.Validation;
using itsmealeseixas.architeture.utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domain.Tests.Entities
{
    public class PersonalTests
    {
        private readonly PersonalValidation personalValition;
        private readonly Faker<Personal> _personalFaker;
   
        public PersonalTests()
        {
            personalValition = new PersonalValidation();
            _personalFaker = new Faker<Personal>("pt_BR")
            .RuleFor(p => p.Name, f => f.Person.FullName)
            .RuleFor(p => p.CreateBy, f => f.Person.Email);
        }


        [Theory]
        [InlineData(20)]
        public void Should_Pass_Validation_When_Model_Is_Valid(int quantidade)
        {
            var personals = _personalFaker.Generate(quantidade);
            var faker = new Faker("pt_BR");
            foreach (var personal in personals)
            {
          
                personal.Document = faker.Person.Cpf(false);
                personal.DateOfBirth = faker.Date.Recent(365 * 99, DateTime.Now.AddYears(-18).Date);
                personal.CreateAt = UtilsHelpers.GetDatetime();
                personal.CreateAtUtc = UtilsHelpers.GetDatetimeUtc();

                var result = personalValition.TestValidate(personal);
                Assert.True(result.IsValid);
            };

            //var personal = TestsUtils.GetValidMockPersonal();



        }

        [Fact]
        public void Should_Fail_Validation_When_Documens_Is_Empty()
        {


            Personal personal = new Personal()
            {
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };


            var result = personalValition.TestValidate(personal);
            Assert.False(result.IsValid);
            Assert.Equal("Document", result.Errors[0].PropertyName);
            Assert.Equal("NotNullValidator", result.Errors[0].ErrorCode);

        }


        [Theory]
        [InlineData("012345678910")]
        [InlineData("11111111111")]
        [InlineData("122345")]
        [InlineData("")]
        [InlineData(null)]
        public void Should_Fail_Validation_When_Documens_Is_Not_Valid(string document)
        {


            Personal personal = new Personal()
            {

                Document = document,
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste"

            };


            var result = personalValition.TestValidate(personal);
            result.ShouldHaveValidationErrorFor(d => d.Document);

        }


        [Theory]
        [InlineData("47897178285")]// AC
        [InlineData("15405583472")]// AL
        [InlineData("79035784219")]// AM
        [InlineData("32666139298")]// AP
        [InlineData("95624854552")]// BA
        [InlineData("43529749354")]// CE
        [InlineData("82010757149")]// DF
        [InlineData("49166239753")]// ES
        [InlineData("55262883150")]// GO
        [InlineData("69997195353")]// MA
        [InlineData("71691291676")]// MG
        [InlineData("08615420106")]// MS
        [InlineData("32305000162")]// MT
        [InlineData("11406168467")]// PA
        [InlineData("43269034490")]// PB
        [InlineData("39497285469")]// PE
        [InlineData("23322359301")]// PI
        [InlineData("34717178971")]// PR
        [InlineData("91366605748")]// RJ
        [InlineData("73666647405")]// RN
        [InlineData("47051813013")]// RS
        [InlineData("99371318201")]// RO
        [InlineData("72439091243")]// RR
        [InlineData("08085847906")]// SC
        [InlineData("20697165540")]// SE
        [InlineData("71621435806")]// SP
        [InlineData("50622834185")]// TO
        public void Should_Pass_Validation_When_Documens_Is_Valid(string document)
        {


            Personal personal = new Personal()
            {
                Document = document,
                Name = "San Nathsa Wulfwen",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };


            var result = personalValition.TestValidate(personal);
            result.ShouldNotHaveValidationErrorFor(d => d.Document);
        }



        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("AlessandroAlessandroAlessandroAlessandroAlessandroAlessandroAlessandroAlessandroAlessandroAlessandro1")]
        public void Should_Fail_Validation_When_Name_Is_Not_Valid(string name)
        {


            Personal personal = new Personal()
            {
                Document = "50622834185",
                Name = name,
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste"
            };


            var result = personalValition.TestValidate(personal);
            result.ShouldHaveValidationErrorFor(d => d.Name);

        }
        
        
        [Theory]
        [InlineData("Alessandro Rosa da Silva Seixas")]
        [InlineData("Luiz Erick Pinto")]
        [InlineData("Tatiane Maya Ribeiro")]
        public void Should_Pass_Validation_When_Name_Is_Valid(string name)
        {


            Personal personal = new Personal()
            {
                Document = "50622834185",
                Name = name,
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };


            var result = personalValition.TestValidate(personal);
            result.ShouldNotHaveValidationErrorFor(d => d.Name);

        }

        [Theory]
        [InlineData("2000-11-12", 2000, 11, 12)] // Data válida
        [InlineData("2000-11-12T08:30:45", 2000, 11, 12)] // Data e hora válidas
        [InlineData("2000-11-12T08:30:45.123", 2000, 11, 12)] // Data e hora com milissegundos válidas
        [InlineData("2000-11-12T08:30:45Z", 2000, 11, 12)] // Data e hora com fuso horário UTC válidas
        public void Should_Pass_Validation_DateOfBirth_Is_Valid(string input, int? expectedYear = null, int? expectedMonth = null, int? expectedDay = null)
        {
            // Arrange & Act
            DateTime? result = TestsUtils.GetValidDate(input);

            // Assert
            if (result.HasValue)
            {
                Assert.Equal(expectedYear, result.Value.Year);
                Assert.Equal(expectedMonth, result.Value.Month);
                Assert.Equal(expectedDay, result.Value.Day);

                Personal personal = new Personal()
                {
                    Document = "50622834185",
                    Name = "Alessandro Rosa da Silva Seixas",
                    DateOfBirth = (DateTime)result,
                    CreateAt = UtilsHelpers.GetDatetime(),
                    CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                    CreateBy = "Teste",
                };

                var resultValidation = personalValition.TestValidate(personal);
                resultValidation.ShouldNotHaveValidationErrorFor(p => p.DateOfBirth);
            }

        }

        [Theory]
        [InlineData("2020-02-29", null)] // Ano bissexto, dia inválido
        [InlineData("2023-13-01", null)] // Mês inválido
        [InlineData("InvalidDate", null)] // Formato inválido
        public void Should_Fail_Validation_Bitrhday_Is_Not_Valid(string input, int? expectedYear = null, int? expectedMonth = null, int? expectedDay = null)
        {
            // Arrange & Act
            DateTime? result = TestsUtils.GetValidDate(input);

            // Assert
            if (!result.HasValue)
            {
                Assert.Null(expectedYear);
                Assert.Null(expectedMonth);
                Assert.Null(expectedDay);

                Personal personal = new Personal()
                {
                    Document = "50622834185",
                    Name = "Alessandro Rosa da Silva Seixas",
                    CreateAt = UtilsHelpers.GetDatetime(),
                    CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                    CreateBy = "Teste",
                };

                var resultValidation = personalValition.TestValidate(personal);
                resultValidation.ShouldHaveValidationErrorFor(p => p.DateOfBirth);
            }

        }

        [Theory]
        [InlineData("2016-11-12", false)] // Menos de 18 anos
        [InlineData("2010-02-29", false)] // Ano bissexto, menos de 18 anos
        public void Should_Fail_Validation_Bitrhday_Under18_Age(string input, bool expectedResult)
        {
            // Arrange & Act
            DateTime? result = TestsUtils.GetValidDate(input);

            // Assert
            if (result.HasValue)
            {

                Personal personal = new Personal()
                {
                    Document = "50622834185",
                    Name = "Alessandro Rosa da Silva Seixas",
                    DateOfBirth = (DateTime)result,
                    CreateAt = UtilsHelpers.GetDatetime(),
                    CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                    CreateBy = "Teste",
                };

                var resultValidation = personalValition.TestValidate(personal);
                resultValidation.ShouldHaveValidationErrorFor(p => p.DateOfBirth);
            }

        }

        [Fact]
        public void Should_Fail_Validation_When_BirthDay_Invalid_WhenNull()
        {
            Personal personal = new Personal()
            {
                Document = "64558658044",
                Name = "San Nathsa Wulfwen",
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste"


            };

            var result = personalValition.TestValidate(personal);
            Assert.False(result.IsValid);
            Assert.Equal("DateOfBirth", result.Errors[0].PropertyName);
            Assert.Equal("NotNullValidator", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void Should_Fail_Validation_When_BirthDay_Invalid_WhenUnder18YearsAgo()
        {
            Personal personal = new Personal()
            {
                Document = "64558658044",
                DateOfBirth = DateTime.Parse("02/04/2016"),
                Name = "San Nathsa Wulfwen",
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            var result = personalValition.TestValidate(personal);
            result.ShouldHaveValidationErrorFor(p => p.DateOfBirth);

        }

        [Fact]
        public void Should_Fail_Validation_When_BirthDay_Valid_WhenOver18YearsAgo()
        {
            Personal personal = new Personal()
            {
                Document = "64558658044",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                Name = "San Nathsa Wulfwen",
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            var result = personalValition.TestValidate(personal);
            result.ShouldNotHaveValidationErrorFor(p => p.DateOfBirth);
        }

    }
}
