using FluentValidation;
using itsmealeseixas.architeture.utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.domain.Seedworks.Abrastracts
{
    public class Validation<TEntity> : AbstractValidator<TEntity>
    {

        public bool BeAValideDateShortDate(string date)
        {
            return UtilsHelpers.BeAValidePT_BRDateShortDate(date);
        }

        public bool BeNumberValidate(string number)
        {
            return UtilsHelpers.BeNumberValidate(number);
        }
        public bool BeAValideDate(DateTime date)
        {
            return UtilsHelpers.BeAValidDate(date);
        }
        public bool CompairAValideDate(DateTime initialDate, DateTime finalDate)
        {
            return UtilsHelpers.CompairAValideDate(initialDate, finalDate);
        }

        public bool BeGuidValidate(string code)
        {

            return UtilsHelpers.BeGuidValidate(code);
        }
        public bool IsCompanyDocumentValid(string cnpj)
        {
            return UtilsHelpers.CheckCNPJ(cnpj);
        }

        public bool IsPersonalDocumentValid(string cpf)
        {
            return UtilsHelpers.CheckCPF(cpf);
        }
        public bool IsPostalCodeValid(string postalcode)
        {
            return UtilsHelpers.CheckBrazilianPostalCode(postalcode);
        }

        public bool IsStateValid(string state)
        {
            return UtilsHelpers.CheckBrazilianStates(state);
        }


        public bool IsCustomerCodeValid(string customercode)
        {
            return UtilsHelpers.ValidateCustomerCode(customercode);
        }

        public bool IsCustomerTokenValid(string customercode)
        {
            return UtilsHelpers.ValidateCustomerToken(customercode);
        }

        public bool IsBankAccountNumberValid(string bankaccountnumbervalid)
        {
            return UtilsHelpers.ValidateAccountNumber(bankaccountnumbervalid);
        }


        public bool IsDocumentProtocolValid(string protocol)
        {
            return UtilsHelpers.ValidateProtocol(protocol);
        }

        public bool IsBase64Valid(string base64Valid)
        {
            return UtilsHelpers.ValidateBase64String(base64Valid);
        }

        public bool IsContractNumberValid(string contractNumber)
        {
            return UtilsHelpers.ValidateUniqueContractNumber(contractNumber);
        }


        public bool BeTimeReferenceValid(string date)
        {
            return UtilsHelpers.BeTimeReferenceValid(date);
        }

    }
}
