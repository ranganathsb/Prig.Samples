using CooperationWithOther;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace CooperationWithOtherTest
{
    [TestFixture]
    public class MoqTest
    {
        [Test]
        public void ValueWithRandomAdded_should_raise_PropertyChanged_event_with_its_name()
        {
            // Arrange
            var notifyingObject = new NotifyingObject();

            var mockHandler = new Mock<PropertyChangedEventHandler>();
            mockHandler.Setup(_ => _(It.IsAny<object>(), It.IsAny<PropertyChangedEventArgs>()));
            notifyingObject.PropertyChanged += mockHandler.Object;

            
            // Act
            notifyingObject.ValueWithRandomAdded = 42;

            
            // Assert
            mockHandler.Verify(_ => _(notifyingObject, It.Is<PropertyChangedEventArgs>(o => o.PropertyName == "ValueWithRandomAdded")), Times.Once());
        }



        [Test]
        public void ValueWithRandomAdded_should_hold_passed_value_plus_random_value()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var notifyingObject = new NotifyingObject();

                var mockNext = new Mock<IndirectionFunc<Random, int>>();
                mockNext.Setup(_ => _(It.IsAny<Random>())).Returns(10);
                PRandom.Next().Body = mockNext.Object;

                
                // Act
                notifyingObject.ValueWithRandomAdded = 32;
                var actual = notifyingObject.ValueWithRandomAdded;

                
                // Assert
                mockNext.Verify(_ => _(It.IsAny<Random>()), Times.Once());
                Assert.AreEqual(42, actual);                
            }
        }
    }
}
