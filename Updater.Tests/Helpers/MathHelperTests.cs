using Updater.Helpers;

namespace Updater.Tests.Helpers
{
    [TestFixture]
    public class MathHelperTests
    {
        [TestCase(-1, 1, ExpectedResult = -100)]
        [TestCase(10, 1, ExpectedResult = 1000)]
        [TestCase(1, 0, ExpectedResult = int.MinValue)]
        public int CalculatePercentageTest(int part, int whole)
        {
            return MathHelper.CalculatePercentage(part, whole);
        }
    }
}
