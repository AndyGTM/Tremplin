using System.ComponentModel.DataAnnotations;
using Tremplin.Data;

namespace Tremplin.CustomValidation
{
    public class ExistingUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DataContext datacontext = (DataContext)validationContext.GetService(typeof(DataContext));

            if (!datacontext.User.Any(x => x.UserName == value.ToString()))
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