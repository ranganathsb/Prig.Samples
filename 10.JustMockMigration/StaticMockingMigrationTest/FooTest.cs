using NUnit.Framework;
using StaticMockingMigration;
using StaticMockingMigration.Prig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Prig;
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
        public void Prig_should_fake_static_property_get()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                PFoo.StaticConstructor().Body = () => { };
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
                var called = false;
                PFoo.StaticConstructor().Body = () => { };
                PFoo.FooPropSetInt32().Body = value => called = true;

                // Act - this line should not throw any mockexception.
                Foo.FooProp = 10;

                // Assert
                Assert.IsTrue(called);
            }
        }
        
        
        
        [Test]
        public void Prig_should_fake_internal_static_call()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                PFooInternal.DoIt().Body = () => called = true;

                // Act
                FooInternal.DoIt();

                // Assert
                Assert.IsTrue(called);
            }
        }



        [Test]
        public void Prig_should_mock_static_class()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                PFooStatic.Do().Body = () => called = true;

                // Act - doesn't throw MockException
                FooStatic.Do();

                // Assert
                Assert.IsTrue(called);
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
    }
}
