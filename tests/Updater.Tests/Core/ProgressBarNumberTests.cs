using Updater.Core;

namespace Updater.Tests.Core
{
    [TestFixture]
    public class ProgressBarNumberTests
    {
        [Test]
        public void ValueTest()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That((int)ProgressBarNumber.One, Is.EqualTo(1));
                Assert.That((int)ProgressBarNumber.Two, Is.EqualTo(2));
            }
        }
    }
}
