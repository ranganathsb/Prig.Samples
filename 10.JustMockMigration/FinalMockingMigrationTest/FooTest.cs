using FinalMockingMigration;
using FinalMockingMigration.Prig;
using Moq;
using NUnit.Framework;
using System;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace FinalMockingMigrationTest
{
    [TestFixture]
    public class FooTest
    {
        [Test]
        public void Prig_should_setup_a_call_to_a_final_method()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var echoInt32 = new Mock<IndirectionFunc<Foo, int, int>>();
                echoInt32.Setup(_ => _(It.IsAny<Foo>(), It.IsAny<int>())).Returns(10);

                var fooProxy = new PProxyFoo();
                fooProxy.EchoInt32().Body = echoInt32.Object;

                var foo = (Foo)fooProxy;


                // Act
                var actual = foo.Echo(1);


                // Assert
                Assert.AreEqual(10, actual);
                echoInt32.Verify(_ => _(It.IsAny<Foo>(), 1), Times.Once());
            }
        }

        [Test]
        public void Prig_should_setup_a_call_to_a_final_property()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooPropGet = new Mock<IndirectionFunc<Foo, string>>();
                fooPropGet.Setup(_ => _(It.IsAny<Foo>())).Returns("bar");

                var fooProxy = new PProxyFoo();
                fooProxy.FooPropGet().Body = fooPropGet.Object;

                var foo = (Foo)fooProxy;


                // Act
                var actual = foo.FooProp;


                // Assert
                Assert.AreEqual("bar", actual);
                fooPropGet.Verify(_ => _(It.IsAny<Foo>()), Times.Once());
            }
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void Prig_should_assert_property_set()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooPropSetString = new Mock<IndirectionAction<Foo, string>>();
                fooPropSetString.Setup(_ => _(It.IsAny<Foo>(), It.Is<string>(s => s != "ping"))).Throws(new NotImplementedException());

                var fooProxy = new PProxyFoo();
                fooProxy.FooPropSetString().Body = fooPropSetString.Object;

                var foo = (Foo)fooProxy;


                // Act
                foo.FooProp = "foo";


                // Assert
                Assert.Fail("We shouldn't get here!!");
            }
        }

        [Test]
        public void Prig_should_assert_on_method_overload()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var executeInt32 = new Mock<IndirectionFunc<Foo, int, int>>();
                executeInt32.Setup(_ => _(It.IsAny<Foo>(), It.IsAny<int>())).Returns<Foo, int>((@this, arg1) => arg1);

                var executeInt32Int32 = new Mock<IndirectionFunc<Foo, int, int, int>>();
                executeInt32Int32.Setup(_ => _(It.IsAny<Foo>(), It.IsAny<int>(), It.IsAny<int>())).Returns<Foo, int, int>((@this, arg1, arg2) => arg1 + arg2);

                var fooProxy = new PProxyFoo();
                fooProxy.ExecuteInt32().Body = executeInt32.Object;
                fooProxy.ExecuteInt32Int32().Body = executeInt32Int32.Object;

                var foo = (Foo)fooProxy;


                // Act
                // nop


                // Assert
                Assert.AreEqual(1, foo.Execute(1));
                Assert.AreEqual(2, foo.Execute(1, 1));
            }
        }

        [Test]
        public void Prig_should_assert_on_method_callbacks()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var handler = default(Foo.EchoEventHandler);

                var addOnEchoCallbackFooEchoEventHandler = new Mock<IndirectionAction<Foo, Foo.EchoEventHandler>>();
                addOnEchoCallbackFooEchoEventHandler.Setup(_ => _(It.IsAny<Foo>(), It.IsAny<Foo.EchoEventHandler>())).Callback<Foo, Foo.EchoEventHandler>((@this, value) => handler += value);

                var echoInt32 = new Mock<IndirectionFunc<Foo, int, int>>();
                echoInt32.Setup(_ => _(It.IsAny<Foo>(), It.IsAny<int>())).Callback<Foo, int>((@this, arg1) => handler(true));


                var fooProxy = new PProxyFoo();
                fooProxy.AddOnEchoCallbackFooEchoEventHandler().Body = addOnEchoCallbackFooEchoEventHandler.Object;
                fooProxy.EchoInt32().Body = echoInt32.Object;

                var foo = (Foo)fooProxy;

                var called = false;
                foo.OnEchoCallback += echoed => called = echoed;


                // Act
                foo.Echo(10);

                
                // Assert
                Assert.IsTrue(called);
            }
        }

        [Test]
        public void Prig_should_assert_on_generic_types_and_method()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var expected = "ping";

                var echoOfTOfTRetT = new Mock<IndirectionFunc<FooGeneric, string, string>>();
                echoOfTOfTRetT.Setup(_ => _(It.IsAny<FooGeneric>(), expected)).Returns<FooGeneric, string>((@this, s) => s);


                var fooGenericProxy = new PProxyFooGeneric();
                fooGenericProxy.EchoOfTOfTRetT<string, string>().Body = echoOfTOfTRetT.Object;

                var fooGeneric = (FooGeneric)fooGenericProxy;


                // Act
                var actual = fooGeneric.Echo<string, string>(expected);


                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
