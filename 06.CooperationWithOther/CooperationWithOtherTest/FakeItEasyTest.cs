using CooperationWithOther;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace CooperationWithOtherTest
{
    [TestFixture]
    public class FakeItEasyTest
    {
        [Test]
        public void ValueWithRandomAdded_should_raise_PropertyChanged_event_with_its_name()
        {
            // Arrange
            var notifyingObject = new NotifyingObject();

            var mockHandler = A.Fake<PropertyChangedEventHandler>();
            A.CallTo(() => mockHandler(A<object>._, A<PropertyChangedEventArgs>._));
            notifyingObject.PropertyChanged += mockHandler;


            // Act
            notifyingObject.ValueWithRandomAdded = 42;


            // Assert
            A.CallTo(() => mockHandler(notifyingObject, A<PropertyChangedEventArgs>.That.Matches(_ => _.PropertyName == "ValueWithRandomAdded"))).MustHaveHappened();
        }



        [Test]
        public void ValueWithRandomAdded_should_hold_passed_value_plus_random_value()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var notifyingObject = new NotifyingObject();

                var mockNext = A.Fake<IndirectionFunc<Random, int>>();
                A.CallTo(() => mockNext(A<Random>._)).Returns(10);
                PRandom.Next().Body = mockNext;


                // Act
                notifyingObject.ValueWithRandomAdded = 32;
                var actual = notifyingObject.ValueWithRandomAdded;


                // Assert
                A.CallTo(() => mockNext(A<Random>._)).MustHaveHappened();
                Assert.AreEqual(42, actual);
            }
        }
    }
}
