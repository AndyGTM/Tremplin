using System.Reflection;

namespace Tremplin.Tests.CustomData
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ComparisonDatesDataSourceAttribute : Attribute, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { "Birth date is earlier than today", new DateTime(1995, 04, 30), true };

            yield return new object[] { "Birth date is today", DateTime.Today, true };

            yield return new object[] { "Birth date is later than today", new DateTime(3095, 04, 30), false };
        }

        public string? GetDisplayName(MethodInfo methodInfo, object?[]? data)
        {
            return data != null ? string.Format("{0} ({1})", data[0], string.Format("{0:MM/dd/yyyy}", data[1])) : null;
        }
    }
}