using FakesMigration;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Net.Prig;
using Urasandesu.Moq.Prig;
using Urasandesu.Moq.Prig.Mixins.Urasandesu.Prig.Framework;
using Urasandesu.Prig.Framework;

namespace FakesMigrationTest
{
    [TestFixture]
    public class WebServiceClientTest
    {
        [Test]
        public void TestThatServiceReturnsAForbiddenStatuscode()
        {
            // Use new IndirectionsContext() instead of ShimsContext.Create().
            using (new IndirectionsContext())
            {
                // Arrange 
                // Use the indirection stub which has name starting with "PProxy" against one instance.
                var requestProxy = new PProxyHttpWebRequest();
                // The indirection stubs "PProxy..." have implicit operator same as Fakes, so you can write as follows: 
                PWebRequest.CreateString().Body = uri => requestProxy;
                // Unlike Fakes, in Prig, "this" is implicitly passed as first parameter of the instance method.
                requestProxy.GetResponse().Body = this1 =>
                {
                    var responseProxy = new PProxyHttpWebResponse();
                    responseProxy.StatusCodeGet().Body = this2 => HttpStatusCode.Forbidden;
                    return responseProxy;
                };
                // Unlike Fakes, Prig tries invoking the original method by default.
                // If you want to make stubs be do-nothing, change the default behavior as follows: 
                requestProxy.ExcludeGeneric().DefaultBehavior = IndirectionBehaviors.DefaultValue;
                var client = new WebServiceClient();
                var url = "testService";
                var expectedResult = false;


                // Act 
                bool actualresult = client.CallWebService(url);


                // Assert 
                Assert.AreEqual(expectedResult, actualresult);
            }
        }



        // **APPENDIX**: This example is corrected my personal unacceptable part by Moq.Prig:
        [Test]
        public void CallWebService_should_return_false_if_HttpStatusCode_is_Forbidden()
        {
            using (new IndirectionsContext())
            {
                // Arrange 
                var ms = new MockStorage(MockBehavior.Strict);

                var requestProxy = new PProxyHttpWebRequest();
                requestProxy.ExcludeGeneric().DefaultBehavior = IndirectionBehaviors.DefaultValue;

                var responseProxy = new PProxyHttpWebResponse();
                responseProxy.StatusCodeGet().Body = @this => HttpStatusCode.Forbidden;
                requestProxy.GetResponse().Body = @this => responseProxy;

                // To improve robustness against unintended modification, you should verify inputs to side-effects by Moq.
                // For example, the original test will pass even if you unintendedly changed the original production code as follows: 
                // var request = CreateWebRequest(url);
                //   -> var request = CreateWebRequest("Foo");
                var url = "testService";
                PWebRequest.CreateString().BodyBy(ms).Expect(_ => _(url)).Returns(requestProxy);


                // Act 
                var actual = new WebServiceClient().CallWebService(url);


                // Assert
                Assert.IsFalse(actual);
                ms.Verify();
            }
        }

        [Test]
        public void CallWebService_should_set_HttpWebRequest_to_request_textxml_content()
        {
            using (new IndirectionsContext())
            {
                // Arrange 
                var ms = new MockStorage(MockBehavior.Strict);

                // And also, you can verify whether HttpWebRequest is set intendedly if you use Moq.
                var requestProxy = new PProxyHttpWebRequest();
                requestProxy.ContentTypeSetString().BodyBy(ms).Expect(_ => _(requestProxy, "text/xml;charset=\"utf-8\""));
                requestProxy.MethodSetString().BodyBy(ms).Expect(_ => _(requestProxy, "GET"));
                requestProxy.TimeoutSetInt32().BodyBy(ms).Expect(_ => _(requestProxy, 1000));
                requestProxy.CredentialsSetICredentials().BodyBy(ms).Expect(_ => _(requestProxy, CredentialCache.DefaultNetworkCredentials));

                var responseProxy = new PProxyHttpWebResponse();
                responseProxy.StatusCodeGet().Body = @this => HttpStatusCode.OK;
                requestProxy.GetResponse().Body = @this => responseProxy;

                var url = "testService";
                PWebRequest.CreateString().Body = @this => requestProxy;


                // Act 
                var actual = new WebServiceClient().CallWebService(url);


                // Assert
                Assert.IsTrue(actual);
                ms.Verify();
            }
        }
    }
}
