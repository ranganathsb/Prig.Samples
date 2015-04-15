using CallingOriginal;
using NUnit.Framework;
using System;
using System.Prig;
using Urasandesu.Prig.Framework;

namespace CallingOriginalTest
{
    [TestFixture]
    public class RicePaddyTest
    {
        [Test]
        public void Constructor_should_be_initialized_by_non_null_if_number_that_is_not_divisible_by_10_is_passed()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var actualValue = 0;
                PRandom.Next().Body = @this => 9;
                PNullable<int>.ConstructorT().Body = (ref Nullable<int> @this, int value) =>
                {
                    actualValue = value;
                    @this = IndirectionsContext.ExecuteOriginal(() => new Nullable<int>(value));
                };


                // Act
                var paddy = new RicePaddy(1, new Random());


                // Assert
                Assert.AreEqual(9000, actualValue);
            }
        }
    }
}
