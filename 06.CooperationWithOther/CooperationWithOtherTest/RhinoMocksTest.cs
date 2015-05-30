using CooperationWithOther;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.ComponentModel;
using System.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace CooperationWithOtherTest
{
    [TestFixture]
    public class RhinoMocksTest
    {
        [Test]
        public void ValueWithRandomAdded_should_raise_PropertyChanged_event_with_its_name()
        {
            // Arrange
            var notifyingObject = new NotifyingObject();

            var mockHandler = MockRepository.GenerateStub<PropertyChangedEventHandler>();
            mockHandler.Stub(_ => _(Arg<object>.Is.Anything, Arg<PropertyChangedEventArgs>.Is.Anything));
            notifyingObject.PropertyChanged += mockHandler;


            // Act
            notifyingObject.ValueWithRandomAdded = 42;


            // Assert
            mockHandler.AssertWasCalled(_ => _(Arg<object>.Is.Same(notifyingObject), Arg<PropertyChangedEventArgs>.Matches(o => o.PropertyName == "ValueWithRandomAdded")), options => options.Repeat.Once());
        }



        [Test]
        public void ValueWithRandomAdded_should_hold_passed_value_plus_random_value()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var notifyingObject = new NotifyingObject();

                var mockNext = MockRepository.GenerateStub<IndirectionFunc<Random, int>>();
                mockNext.Stub(_ => _(Arg<Random>.Is.Anything)).Return(10);
                PRandom.Next().Body = mockNext;


                // Act
                notifyingObject.ValueWithRandomAdded = 32;
                var actual = notifyingObject.ValueWithRandomAdded;


                // Assert
                mockNext.AssertWasCalled(_ => _(Arg<Random>.Is.Anything), options => options.Repeat.Once());
                Assert.AreEqual(42, actual);
            }
        }
    }
}
