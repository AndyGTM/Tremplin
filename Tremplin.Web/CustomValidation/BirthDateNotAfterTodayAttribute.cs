using System.ComponentModel.DataAnnotations;

namespace Tremplin.CustomValidation
{
    public class BirthDateNotAfterTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int comparisonResult = DateTime.Compare((DateTime)value, DateTime.Today);

            if (comparisonResult <= 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}