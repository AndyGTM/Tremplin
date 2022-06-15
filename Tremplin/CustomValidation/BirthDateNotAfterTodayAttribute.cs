using System.ComponentModel.DataAnnotations;

namespace Tremplin.CustomValidation
{
    /// <summary>
    /// Checking if the date of birth is not after today's date
    /// </summary>
    public class BirthDateNotAfterTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int comparisonResult = DateTime.Compare((DateTime)value, DateTime.Today);

            // BirthDate earlier or equal to today
            if (comparisonResult <= 0)
            {
                return true;
            }

            // BirthDate later to today
            else
            {
                return false;
            }
        }
    }
}