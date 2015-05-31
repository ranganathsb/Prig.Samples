using Moq;
using NUnit.Framework;
using StaticMockingMigration;
using StaticMockingMigration.Prig;
using System.Web;
using System.Web.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace StaticMockingMigrationTest
{
    [TestFixture]
    public class FooTest
    {
        [Test]
        public void Prig_should_arrange_static_function()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFoo.StaticConstructor().Body = () => { };
                PFoo.FooPropGet().Body = () => 0;


                // Act
                var actual = Foo.FooProp;


                // Assert
                Assert.AreEqual(0, actual);
            }
        }



        [Test]
        public void Prig_should_throw_when_not_arranged()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFoo.StaticConstructor().Body = () => { };

                var executeMock = new Mock<IndirectionFunc<int, int>>(MockBehavior.Strict);
                executeMock.Setup(_ => _(10)).Returns(10);
                PFoo.ExecuteInt32().Body = executeMock.Object;

                var submitMock = new Mock<IndirectionAction>(MockBehavior.Strict);
                PFoo.Submit().Body = submitMock.Object;


                // Act, Assert
                Assert.AreEqual(10, Foo.Execute(10));
                Assert.Throws<MockException>(() => Foo.Submit());
            }
        }

        
        
        [Test]
        public void Prig_should_fake_static_property_get()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFoo.StaticConstructor().Body = () => { };

                var called = false;
                PFoo.FooPropGet().Body = () => { called = true; return 1; };


                // Act
                var actual = Foo.FooProp;


                // Assert
                Assert.AreEqual(1, actual);
                Assert.IsTrue(called);
            }
        }



        [Test]
        public void Prig_should_fake_static_property_set()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFoo.StaticConstructor().Body = () => { };

                var fooPropSetMock = new Mock<IndirectionAction<int>>(MockBehavior.Strict);
                fooPropSetMock.Setup(_ => _(10));
                PFoo.FooPropSetInt32().Body = fooPropSetMock.Object;


                // Act, Assert
                Foo.FooProp = 10;
            }
        }
        
        
        
        [Test]
        public void Prig_should_fake_internal_static_call()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFooInternal.DoIt().Body = () => { };


                // Act, Assert
                Assert.DoesNotThrow(() => FooInternal.DoIt());
            }
        }



        [Test]
        public void Prig_should_mock_static_class()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFooStatic.Do().Body = () => { };


                // Act, Assert
                Assert.DoesNotThrow(() => FooStatic.Do());
            }
        }



        [Test]
        public void Prig_should_assert_mocking_http_context()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                PHttpContext.CurrentGet().Body = () => { called = true; return null; };


                // Act
                var ret = HttpContext.Current;


                // Assert
                Assert.IsTrue(called);
            }
        }



        [Test]
        public void Prig_should_fake_extension_method()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var foo = new Bar();

                var echoMock = new Mock<IndirectionFunc<Bar, int, int>>();
                echoMock.Setup(_ => _(foo, 10)).Returns(11);
                PBarExtensions.EchoBarInt32().Body = echoMock.Object;


                // Act
                var actual = foo.Echo(10);


                // Assert
                Assert.AreEqual(11, actual);
            }
        }
    }
}
