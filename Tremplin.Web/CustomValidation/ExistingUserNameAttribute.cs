using System.ComponentModel.DataAnnotations;
using Tremplin.Data;

namespace Tremplin.CustomValidation
{
    /// <summary>
    /// Checking if there is an identical username in the database
    /// </summary>
    public class ExistingUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DataContext datacontext = (DataContext)validationContext.GetService(typeof(DataContext));

            if (!datacontext.Users.Any(x => x.UserName == value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Cet identifiant existe déjà");
            }
        }
    }
}