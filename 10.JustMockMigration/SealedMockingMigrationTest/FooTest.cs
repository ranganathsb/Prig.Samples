using Moq;
using NUnit.Framework;
using SealedMockingMigration;
using SealedMockingMigration.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace SealedMockingMigrationTest
{
    [TestFixture]
    public class FooTest
    {
        [Test]
        public void Prig_should_assert_final_method_call_on_a_sealed_class_with_Moq()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooMock = new Mock<IndirectionFunc<FooSealed, int, int>>();
                fooMock.Setup(_ => _(It.IsAny<FooSealed>(), It.IsAny<int>())).Returns(10);

                var fooProxy = new PProxyFooSealed();
                fooProxy.EchoInt32().Body = fooMock.Object;
                var foo = (FooSealed)fooProxy;

                // Act
                var actual = foo.Echo(1);

                // Assert
                Assert.AreEqual(10, actual);
                fooMock.Verify(_ => _(foo, 1), Times.Once());
            }
        }

        [Test]
        public void Prig_should_assert_final_method_call_on_a_sealed_class_with_Moq2()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooMock = new Mock<IndirectionFunc<FooSealed, int, int>>();
                fooMock.Setup(_ => _(It.IsAny<FooSealed>(), It.IsAny<int>())).Returns(10);

                var fooProxy = new PProxyFooSealed();
                fooProxy.EchoInt32().Body = fooMock.Object;
                var foo = (FooSealed)fooProxy;

                // Act
                var actual = foo.Echo(1);

                // Assert
                Assert.AreEqual(10, actual);
            }
        }

        [Test]
        public void Prig_should_assert_final_method_call_on_a_sealed_class_without_Moq()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooProxy = new PProxyFooSealed();
                fooProxy.EchoInt32().Body = (@this, arg1) => 10;
                var foo = (FooSealed)fooProxy;

                // Act
                var actual = foo.Echo(1);

                // Assert
                Assert.AreEqual(10, actual);
            }
        }

        [Test]
        public void Prig_should_assert_final_method_call_on_a_sealed_class_without_Moq2()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var callCount = 0;
                var actualThis = default(FooSealed);
                var actualArg1 = default(int);

                var fooProxy = new PProxyFooSealed();
                fooProxy.EchoInt32().Body = (@this, arg1) =>
                {
                    callCount++;
                    actualThis = @this;
                    actualArg1 = arg1;
                    return 10;
                };
                var foo = (FooSealed)fooProxy;

                // Act
                var actual = foo.Echo(1);

                // Assert
                Assert.AreEqual(10, actual);
                Assert.AreSame(foo, actualThis);
                Assert.AreEqual(1, actualArg1);
                Assert.AreEqual(1, callCount);
            }
        }



        [Test]
        public void Prig_should_create_mock_for_a_sealed_class_with_internal_constructor()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooMock = new Mock<IndirectionFunc<FooSealedInternal, int, int>>();
                fooMock.Setup(_ => _(It.IsAny<FooSealedInternal>(), It.IsAny<int>())).Returns(10);

                var fooProxy = new PProxyFooSealedInternal();
                fooProxy.EchoInt32().Body = fooMock.Object;
                var foo = (FooSealedInternal)fooProxy;


                // Act
                var actual = foo.Echo(1);


                // Assert
                Assert.AreEqual(10, actual);
            }
        }

        [Test]
        public void Prig_should_create_mock_for_a_sealed_class_with_internal_constructor_without_Moq()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var fooProxy = new PProxyFooSealedInternal();
                fooProxy.EchoInt32().Body = (@this, arg1) => 10;
                var foo = (FooSealedInternal)fooProxy;


                // Act
                var actual = foo.Echo(1);


                // Assert
                Assert.AreEqual(10, actual);
            }
        }



        [Test]
        public void Prig_should_assert_call_on_void()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                var fooProxy = new PProxyFoo();
                fooProxy.Execute().Body = @this => called = true;
                var foo = (Foo)fooProxy;

                // Act
                foo.Execute();

                // Assert
                Assert.IsTrue(called);
            }
        }



        [Test]
        public void Prig_should_assert_call_on_void_through_an_interface()
        {
            // Arrange
            var called = false;
            var fooProxy = new PProxyFoo();
            fooProxy.Execute().Body = @this => called = true;
            var foo = (Foo)fooProxy;

            // Act
            var iFoo = (IFoo)foo;
            iFoo.Execute();

            // Assert
            Assert.IsTrue(called);
        }
    }
}
