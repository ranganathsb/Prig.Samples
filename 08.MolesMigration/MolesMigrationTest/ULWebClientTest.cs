using MolesMigration;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Prig;
using System.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace MolesMigrationTest
{
    [TestFixture]
    public class ULWebClientTest
    {
        [Test]
        public void ShowGoogle_should_write_response_from_google_to_standard_output()
        {
            // Prig has no attributes like HostType("Moles"). Use using (new IndirectionsContext()) instead of that.
            using (new IndirectionsContext())
            {
                // Arrange 
                var handler = default(DownloadStringCompletedEventHandler);
                handler = (sender, e) => { };

                // AllInstances that is the feature of Moles to mock all instance members doesn't exist, because it is default feature.
                PWebClient.AddDownloadStringCompletedDownloadStringCompletedEventHandler().Body = (@this, value) => handler += value;
                PWebClient.RemoveDownloadStringCompletedDownloadStringCompletedEventHandler().Body = (@this, value) => handler -= value;
                PWebClient.DownloadStringAsyncUri().Body = (@this, address) =>
                {
                    // Use the stub that starts with PProxy if you want to mock against one instance.
                    var e = new PProxyDownloadStringCompletedEventArgs();
                    e.ResultGet().Body = @this_ => "google!modoki";
                    handler(@this, e);
                };
                var mockWriteLine = new Mock<IndirectionAction<string>>();
                mockWriteLine.Setup(_ => _(It.IsAny<string>()));
                PConsole.WriteLineString().Body = mockWriteLine.Object;


                // Act 
                ULWebClient.ShowGoogle();


                // Assert 
                mockWriteLine.Verify(_ => _("google!modoki"), Times.Once());
            }
        }
    }
}
