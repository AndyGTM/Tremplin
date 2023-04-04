using Tremplin.Tests.CustomData;

namespace Tremplin.Tests.Helpers
{
    [TestClass]
    public class ComparisonDatesHelperTest
    {
        [DataTestMethod]
        [ComparisonDatesDataSource]
        public void Compare_BirthDate_WithToday(string displayTestRowName, DateTime birthDate, int expectedComparison)
        {
            int currentComparison = DateTime.Compare(birthDate, DateTime.Today);

            Assert.AreEqual(expectedComparison, currentComparison);
        }
    }
}