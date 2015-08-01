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
                var fooProxy = new PProxyFoo();
                fooProxy.EchoInt32().Body = (@this, arg1) => 10;
                var foo = (Foo)fooProxy;


                // Act
                var actual = foo.Echo(1);


                // Assert
                Assert.AreEqual(10, actual);
            }
        }

        [Test]
        public void Prig_should_setup_a_call_to_a_final_property()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooProxy = new PProxyFoo();
                fooProxy.FooPropGet().Body = @this => "bar";
                var foo = (Foo)fooProxy;


                // Act
                var actual = foo.FooProp;


                // Assert
                Assert.AreEqual("bar", actual);
            }
        }

        [Test]
        [ExpectedException(typeof(MockException))]
        public void Prig_should_assert_property_set()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooProxy = new PProxyFoo();
                var fooPropSetMock = new Mock<IndirectionAction<Foo, string>>(MockBehavior.Strict);
                fooPropSetMock.Setup(_ => _(fooProxy, "ping"));
                fooProxy.FooPropSetString().Body = fooPropSetMock.Object;
                var foo = (Foo)fooProxy;


                // Act, Assert
                foo.FooProp = "foo";
            }
        }

        [Test]
        public void Prig_should_assert_on_method_overload()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooProxy = new PProxyFoo();
                fooProxy.ExecuteInt32().Body = (@this, arg1) => arg1;
                fooProxy.ExecuteInt32Int32().Body = (@this, arg1, arg2) => arg1 + arg2;
                var foo = (Foo)fooProxy;


                // Act, Assert
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

                var fooProxy = new PProxyFoo();
                fooProxy.AddOnEchoCallbackFooEchoEventHandler().Body = (@this, value) => handler += value;
                fooProxy.EchoInt32().Body = (@this, arg1) => { handler(true); return arg1; };

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

                var fooGenericProxy = new PProxyFooGeneric();
                fooGenericProxy.EchoOfTOfTRetT<string, string>().Body = (@this, s) => s;

                var fooGeneric = (FooGeneric)fooGenericProxy;


                // Act
                var actual = fooGeneric.Echo<string, string>(expected);


                // Assert
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
