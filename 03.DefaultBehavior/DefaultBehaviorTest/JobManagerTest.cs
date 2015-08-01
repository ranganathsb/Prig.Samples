using DefaultBehavior;
using DefaultBehavior.Prig;
using Moq;
using NUnit.Framework;
using System.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace DefaultBehaviorTest
{
    [TestFixture]
    public class JobManagerTest
    {
        [Test]
        public void NotifyStartJob_should_verify_using_the_methods_of_CommunicationContext_then_UpdateJobParameterFile()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                // Set default behavior for most methods of Environment.
                PEnvironment.
                    ExcludeGeneric().
                    // Environment.CurrentManagedThreadId is used by Mock<T>.Setup<TResult>(Expression<Func<T, TResult>>).
                    Exclude(PEnvironment.CurrentManagedThreadIdGet()).
                    // Environment.OSVersion is used by Times.Once().
                    Exclude(PEnvironment.OSVersionGet()).
                    DefaultBehavior = IndirectionBehaviors.NotImplemented;

                var mockCtx = new Mock<CommunicationContext>();
                mockCtx.Setup(_ => _.VerifyRuntimeVersion(It.IsAny<string[]>())).Returns(0);
                mockCtx.Setup(_ => _.VerifyPrereqOf3rdParty(It.IsAny<string[]>())).Returns(0);
                mockCtx.Setup(_ => _.VerifyUserAuthority(It.IsAny<string[]>())).Returns(0);
                mockCtx.Setup(_ => _.VerifyProductLicense(It.IsAny<string[]>())).Returns(0);
                var ctx = mockCtx.Object;

                var args = new[] { "foo", "bar", "baz", "qux" };

                var mockUpdateJobParameterFile = new Mock<IndirectionAction<int, int, bool, string>>();
                PJobManager.UpdateJobParameterFileInt32Int32BooleanString().Body = mockUpdateJobParameterFile.Object;

                // Act
                JobManager.NotifyStartJob(ctx, args);

                // Assert
                mockCtx.Verify(_ => _.VerifyRuntimeVersion(args), Times.Once());
                mockCtx.Verify(_ => _.VerifyPrereqOf3rdParty(args), Times.Once());
                mockCtx.Verify(_ => _.VerifyUserAuthority(args), Times.Once());
                // VerifyProductLicense will never be called, so change verification Once() -> Never().
                mockCtx.Verify(_ => _.VerifyProductLicense(args), Times.Never());
                mockUpdateJobParameterFile.Verify(_ => _(0, 0, false, null), Times.Once());
            }
        }
    }
}
