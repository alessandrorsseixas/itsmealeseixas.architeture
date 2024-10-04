using FluentValidation;
using Its.Me.AleSeixas.Example.Domina.Entities;
using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domina.Validation
{
    public class CustomerValidation : EntityValidation<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Code)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
             .Length(8).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres").WithSeverity(Severity.Error).WithErrorCode("MinOrMaxLength")
             .Must(IsCustomerCodeValid).WithMessage("Não é um codigo válido").WithSeverity(Severity.Warning).WithErrorCode("CustomerCodeIsNotValid")
             .WithName("CÓDIGO DO CLIENTE");

            RuleFor(c => c.Token)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
             .Length(6).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres").WithSeverity(Severity.Error).WithErrorCode("MinOrMaxLength")
             .Must(IsCustomerTokenValid).WithMessage("Não é um token válido").WithSeverity(Severity.Warning).WithErrorCode("CustomerTokenIsNotValid")
             .WithName("TOKEN DO CLIENTE");
        }
    }
}
