using Bogus;
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
    public class CustomerTests
    {
        private readonly CustomerValidation validations;
        private readonly Faker<Customer> _customerFaker;
        public CustomerTests()
        {
            validations = new CustomerValidation();
            _customerFaker = new Faker<Customer>()
            .RuleFor(c => c.Code, f => UtilsHelpers.GenerateUniqueCustomerCode())
            .RuleFor(c => c.Token, f => UtilsHelpers.GenerateUniqueCustomerToken())
            .RuleFor(c => c.IsActive, f => f.Random.Bool())
            .RuleFor(c => c.IdPersonal, f => f.Random.Guid())
            .RuleFor(c => c.Personal, f => new Personal());
        }

        [Theory]
        [InlineData(20)]
        public void Should_Pass_Validation_When_Model_Is_Valid(int quantity)
        {
            var customers = _customerFaker.Generate(quantity);
            foreach (var item in customers)
            {
                item.CreateBy = "Faker";
                item.CreateAt = UtilsHelpers.GetDatetime();
                item.CreateAtUtc = UtilsHelpers.GetDatetimeUtc();
                var result = validations.TestValidate(item);
                Assert.True(result.IsValid);
            }

        }



        [Theory]
        [InlineData("ABC123")]
        [InlineData("xyz789")]
        [InlineData("123456")]
        public void Should_Fail_Validation_When_Token_Is_Valid(string token)
        {

            var customer = TestsUtils.GetCustomerMock();
            customer.Token = token;

            var result = validations.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(x => x.Token);
        }




        [Theory]
        [InlineData("AB12")] // Less than 6 characters
        [InlineData("XYZ1234")] // More than 6 characters
        [InlineData("ABC@123")] // Contains non-alphanumeric characters
        [InlineData(" ")] // Empty string
        public void Should_Fail_Validation_When_Code_Is_Not_Valid(string code)
        {

            var customer = TestsUtils.GetCustomerMock();
            customer.Code = code;

            var result = validations.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(x => x.Code);
        }

    }
}
