using Updater.Helpers;

namespace Updater.Tests.Helpers
{
    [TestFixture]
    public class MathHelperTests
    {
        [Test]
        public void ShouldCalculatePercentage()
        {
            for (int i = 1; i < 10; i++)
            {
                var percentage = MathHelper.CalculatePercentage(i, 10);
                Assert.That(percentage, Is.EqualTo(i * 10));
            }

            for (int i = -10; i < 0; i++)
            {
                var percentage = MathHelper.CalculatePercentage(i, 10);
                Assert.That(percentage, Is.EqualTo(i * 10));
            }
        }
    }
}
