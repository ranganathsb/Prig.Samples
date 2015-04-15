using NUnit.Framework;
using QuickTour;
using System;
using System.Prig;
using Urasandesu.Prig.Framework;

namespace QuickTourTest
{
    [TestFixture]
    public class LifeInfoTest
    {
        [Test]
        public void IsNowLunchBreak_should_return_false_when_11_oclock()
        {
            // `IndirectionsContext` can minimize the influence of the API replacement.
            using (new IndirectionsContext())
            {
                // Arrange
                // Replace `DateTime.Now` body. Hereafter, `DateTime.Now` will return only `2013/12/13 11:00:00`.
                PDateTime.NowGet().Body = () => new DateTime(2013, 12, 13, 11, 00, 00);

                // Act
                var result = LifeInfo.IsNowLunchBreak();

                // Assert
                Assert.IsFalse(result);
            }
        }

        // In the same way, add the test case to cover other branches...
        [Test]
        public void IsNowLunchBreak_should_return_true_when_12_oclock()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PDateTime.NowGet().Body = () => new DateTime(2013, 12, 13, 12, 00, 00);

                // Act
                var result = LifeInfo.IsNowLunchBreak();

                // Assert
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void IsNowLunchBreak_should_return_false_when_13_oclock()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PDateTime.NowGet().Body = () => new DateTime(2013, 12, 13, 13, 00, 00);

                // Act
                var result = LifeInfo.IsNowLunchBreak();

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}
