using System.ComponentModel.DataAnnotations;
using Tremplin.Core.Helpers;
using Tremplin.Data;

namespace Tremplin.CustomValidation
{
    public class ExistingSocialSecurityNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            DataContext datacontext = (DataContext)validationContext.GetService(typeof(DataContext));

            string socialSecurityNumberViewModel = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(value.ToString());

            if (!datacontext.Patient.Any(x => x.SocialSecurityNumber == socialSecurityNumberViewModel))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Ce numéro existe déjà");
            }
        }
    }
}