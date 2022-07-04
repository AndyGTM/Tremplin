using System.ComponentModel.DataAnnotations;
using Tremplin.Data;

namespace Tremplin.CustomValidation
{
    /// <summary>
    /// Checking if there is an identical email in the database
    /// </summary>
    public class ExistingMailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DataContext datacontext = (DataContext)validationContext.GetService(typeof(DataContext));

            if (!datacontext.User.Any(x => x.Email == value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Cette adresse e-mail existe déjà");
            }
        }
    }
}