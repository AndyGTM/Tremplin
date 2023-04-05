using System.ComponentModel.DataAnnotations;
using Tremplin.Core.Helpers;

namespace Tremplin.CustomValidation
{
    public class BirthDateBeforeOrEqualToTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool comparisonResult = ComparisonDatesHelper.IsBeforeOrEqualToToday((DateTime)value);

            return comparisonResult;
        }
    }
}