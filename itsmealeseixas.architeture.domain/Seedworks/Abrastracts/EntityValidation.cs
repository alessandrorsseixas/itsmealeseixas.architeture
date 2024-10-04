using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.domain.Seedworks.Abrastracts
{
    public class EntityValidation<TEntity> : Validation<TEntity> where TEntity : Entity
    {
        public EntityValidation()
        {

            RuleFor(c => c.CreateBy)
           .NotEmpty()
           .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres")
           .WithName("USUÁRIO CADASTRO");

            RuleFor(c => c.CreateAt)
            .NotEmpty().WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
            .Must(BeAValideDate).WithMessage("Data Cadastro é um campo obrigatório")
            .WithName("DATA CADASTRO");

            RuleFor(c => c.CreateAtUtc)
             .NotEmpty().WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
             .Must(BeAValideDate).WithMessage("Data Cadastro UTC é um campo obrigatório")
             .WithName("DATA CADASTRO UTC");
            // RuleFor(c => c.UpdateBy)
            //.Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres")
            //.WithName("USUÁRIO ATUALIZAÇÃO");

            // RuleFor(c => c.UpdateAt)
            // .WithName("DATA CADASTRO");

            // RuleFor(c => c.CreateAtUtc)
            //  .NotEmpty().WithSeverity(Severity.Error).WithErrorCode("NotNullValidator")
            //  .Must(BeAValideDate).WithMessage("Data Cadastro UTC é um campo obrigatório")
            //  .WithName("DATA CADASTRO UTC");
        }
    }
}
