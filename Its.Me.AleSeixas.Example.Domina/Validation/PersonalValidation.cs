using FluentValidation;
using Its.Me.AleSeixas.Example.Domina.Entities;
using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domina.Validation
{
    public class PersonalValidation : EntityValidation<Personal>
    {
        public PersonalValidation()
        {
            RuleFor(c => c.Document)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
           .Length(11).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres").WithSeverity(Severity.Error).WithErrorCode("MinOrMaxLength")
           .Must(IsPersonalDocumentValid).WithMessage("Não é um CPF válido").WithSeverity(Severity.Warning).WithErrorCode("PersonalDocumentIsNotValid")
           .WithName("CPF");


            RuleFor(c => c.Name)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
           .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres").WithSeverity(Severity.Error).WithErrorCode("MinOrMaxLength")
           .WithName("NOME");

            RuleFor(p => p.DateOfBirth)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido").WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("A pessoa deve ter pelo menos 18 anos de idade.").WithErrorCode("InvalidAge")
            .WithName("DATA NASCIMENTO"); 

        }

       


    }
}
