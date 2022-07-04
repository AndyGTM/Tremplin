using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tremplin.Data;

namespace Tremplin.CustomValidation
{
    /// <summary>
    /// Checking if there is an identical social security number in the database
    /// </summary>
    public class ExistingSocialSecurityNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            DataContext datacontext = (DataContext)validationContext.GetService(typeof(DataContext));

            // Removal of any blank spaces to match database social security number
            string socialSecurityNumberViewModel = Regex.Replace(value.ToString(), @"\s", "");

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