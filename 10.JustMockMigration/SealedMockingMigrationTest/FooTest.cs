using NUnit.Framework;
using SealedMockingMigration;
using SealedMockingMigration.Prig;
using Urasandesu.Prig.Framework;

namespace SealedMockingMigrationTest
{
    [TestFixture]
    public class FooTest
    {
        [Test]
        public void Prig_should_assert_final_method_call_on_a_sealed_class()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFooSealed.EchoInt32().Body = (@this, arg1) => 10;


                // Act
                var actual = new FooSealed().Echo(1);


                // Assert
                Assert.AreEqual(10, actual);
            }
        }



        [Test]
        public void Prig_should_create_mock_for_a_sealed_class_with_internal_constructor()
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
                PFoo.Execute().Body = @this => called = true;


                // Act
                new Foo().Execute();


                // Assert
                Assert.IsTrue(called);
            }
        }



        [Test]
        public void Prig_should_assert_call_on_void_through_an_interface()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                PFoo.Execute().Body = @this => called = true;
                var foo = new Foo();


                // Act
                var iFoo = (IFoo)foo;
                iFoo.Execute();


                // Assert
                Assert.IsTrue(called);
            }
        }



        [Test]
        public void Prig_should_assert_call_on_void_through_an_explict_implemented_interface()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var called = false;
                PFoo.SealedMockingMigrationIFooExecuteInt32().Body = (@this, arg1) => called = true;
                var foo = new Foo();


                // Act
                var iFoo = (IFoo)foo;
                iFoo.Execute(1);


                // Assert
                Assert.IsTrue(called);
            }
        }
    }
}
