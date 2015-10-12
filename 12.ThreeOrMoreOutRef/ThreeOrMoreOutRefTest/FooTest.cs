using NUnit.Framework;
using System;
using ThreeOrMoreOutRef;
using ThreeOrMoreOutRef.Prig;
using Urasandesu.Prig.Framework;

namespace ThreeOrMoreOutRefTest
{
    [TestFixture]
    public class FooTest
    {
        [Test]
        public void FormatCurrentThreadTimes_on_execute_should_return_expected()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PFoo.GetThreadTimesIntPtrInt64RefInt64RefInt64RefInt64Ref().Body = 
                    (IntPtr hThread, out long lpCreationTime, out long lpExitTime, out long lpKernelTime, out long lpUserTime) =>
                    {
                        lpCreationTime = 0;
                        lpExitTime = 1;
                        lpKernelTime = 2;
                        lpUserTime = 3;
                        return true;
                    };

                
                // Act
                var actual = new Foo().FormatCurrentThreadTimes();

                
                // Assert
                Assert.AreEqual("Creation Time: 0, Exit Time: 1, Kernel Time: 2, User Time: 3", actual);
            }
        }
    }
}
