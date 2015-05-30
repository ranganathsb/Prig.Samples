using CooperationWithOther;
using NSubstitute;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace CooperationWithOtherTest
{
    [TestFixture]
    public class NSubstituteTest
    {
        [Test]
        public void ValueWithRandomAdded_should_raise_PropertyChanged_event_with_its_name()
        {
            // Arrange
            var notifyingObject = new NotifyingObject();

            var mockHandler = Substitute.For<PropertyChangedEventHandler>();
            mockHandler(Arg.Any<object>(), Arg.Any<PropertyChangedEventArgs>());
            notifyingObject.PropertyChanged += mockHandler;


            // Act
            notifyingObject.ValueWithRandomAdded = 42;


            // Assert
            mockHandler.Received(1)(notifyingObject, Arg.Is<PropertyChangedEventArgs>(o => o.PropertyName == "ValueWithRandomAdded"));
        }



        [Test]
        public void ValueWithRandomAdded_should_hold_passed_value_plus_random_value()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var notifyingObject = new NotifyingObject();

                var mockNext = Substitute.For<IndirectionFunc<Random, int>>();
                mockNext(Arg.Any<Random>()).Returns(10);
                PRandom.Next().Body = mockNext;


                // Act
                notifyingObject.ValueWithRandomAdded = 32;
                var actual = notifyingObject.ValueWithRandomAdded;


                // Assert
                mockNext.Received(1)(Arg.Any<Random>());
                Assert.AreEqual(42, actual);
            }
        }
    }
}
