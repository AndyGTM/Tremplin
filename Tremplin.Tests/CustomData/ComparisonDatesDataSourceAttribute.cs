using System.Reflection;

namespace Tremplin.Tests.CustomData
{
    public class ComparisonDatesDataSourceAttribute : Attribute, ITestDataSource
    {
        /// <summary>
        /// Collection of objects with dates, integers and strings used for testing the "DateTime.Compare()" method
        /// </summary>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            // Earlier date than today (integer value is then equal to -1 when using "DateTime.Compare()")
            yield return new object[] { new DateTime(1995, 04, 30), -1, "Birth date is earlier than today" };

            // Same date as today (integer value is then equal to 0 when using "DateTime.Compare()")
            yield return new object[] { DateTime.Today, 0, "Birth date is today" };

            // Later date than today (integer value is then equal to 1 when using "DateTime.Compare()")
            yield return new object[] { new DateTime(3095, 04, 30), 1, "Birth date is later than today" };
        }

        /// <summary>
        /// Display names of data rows for comparison dates in the Test Explorer
        /// </summary>
        /// <param name="data">Object with date, integer and string, corresponding to a row for comparison dates in the Test Explorer</param>
        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (data != null)
                return string.Format("{0} ({1})", data[2], string.Format("{0:MM/dd/yyyy}", data[0]));

            return null;
        }
    }
}
