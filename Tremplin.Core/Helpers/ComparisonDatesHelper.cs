namespace Tremplin.Core.Helpers
{
    public static class ComparisonDatesHelper
    {
        public static bool IsBeforeOrEqualToToday(DateTime birthDate)
        {
            int comparisonResult = DateTime.Compare(birthDate, DateTime.Today);

            return comparisonResult <= 0;
        }
    }
}