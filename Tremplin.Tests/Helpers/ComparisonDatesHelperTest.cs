using Tremplin.Core.Helpers;
using Tremplin.Tests.CustomData;

namespace Tremplin.Tests.Helpers
{
    [TestClass]
    public class ComparisonDatesHelperTest
    {
        [DataTestMethod]
        [ComparisonDatesDataSource]
        public void Compare_BirthDate_WithToday(string displayTestRowName, DateTime birthDate, bool expectedComparison)
        {
            bool currentComparison = ComparisonDatesHelper.IsBeforeOrEqualToToday(birthDate);

            Assert.AreEqual(expectedComparison, currentComparison);
        }
    }
}