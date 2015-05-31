using FakesMigration;
using FakesMigrationTest.FakesMigrationTestDetail;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Prig;
using System.Reflection;
using Urasandesu.Prig.Delegates;
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



        // **APPENDIX**: This example is corrected my personal unacceptable part by Moq:
        [Test]
        public void CallWebService_should_return_false_if_HttpStatusCode_is_Forbidden()
        {
            using (new IndirectionsContext())
            {
                // Arrange 
                var requestProxy = new PProxyHttpWebRequest();

                requestProxy.ExcludeGeneric().DefaultBehavior = IndirectionBehaviors.DefaultValue;

                var responseProxy = new PProxyHttpWebResponse();
                responseProxy.StatusCodeGet().Body = @this => HttpStatusCode.Forbidden;
                requestProxy.GetResponse().Body = @this => responseProxy;

                // To improve robustness against unintended modification, you should verify inputs to side-effects by Moq.
                // For example, the original test will pass even if you unintendedly changed the original production code as follows: 
                // var request = CreateWebRequest(url);
                //   -> var request = CreateWebRequest("Foo");
                var webRequestMock = new Mock<IndirectionFunc<string, WebRequest>>();
                webRequestMock.Setup(_ => _(It.IsAny<string>())).Returns(requestProxy);
                PWebRequest.CreateString().Body = webRequestMock.Object;

                var client = new WebServiceClient();
                var url = "testService";


                // Act 
                var actual = client.CallWebService(url);


                // Assert
                Assert.IsFalse(actual);
                webRequestMock.Verify(_ => _(url), Times.Once());
            }
        }

        [Test]
        public void CallWebService_should_set_HttpWebRequest_to_request_textxml_content()
        {
            using (new IndirectionsContext())
            {
                // Arrange 
                // And also, you can verify whether HttpWebRequest is set intendedly if you use Moq.
                var requestProxy = new PProxyHttpWebRequest();

                var mocks = new MockRepository(MockBehavior.Default);
                mocks.Create(requestProxy.ContentTypeSetString(), _ => _.Body).Setup(_ => _(requestProxy, "text/xml;charset=\"utf-8\""));
                mocks.Create(requestProxy.MethodSetString(), _ => _.Body).Setup(_ => _(requestProxy, "GET"));
                mocks.Create(requestProxy.TimeoutSetInt32(), _ => _.Body).Setup(_ => _(requestProxy, 1000));
                mocks.Create(requestProxy.CredentialsSetICredentials(), _ => _.Body).Setup(_ => _(requestProxy, CredentialCache.DefaultNetworkCredentials));

                var responseProxy = new PProxyHttpWebResponse();
                responseProxy.StatusCodeGet().Body = @this => HttpStatusCode.OK;
                requestProxy.GetResponse().Body = @this => responseProxy;

                PWebRequest.CreateString().Body = @this => requestProxy;

                var client = new WebServiceClient();
                var url = "testService";


                // Act 
                var actual = client.CallWebService(url);


                // Assert
                Assert.IsTrue(actual);
                mocks.VerifyAll();
            }
        }
    }

    namespace FakesMigrationTestDetail
    {
        // The type annotation that specifies to Moq tends to be intrusive because it is based on a delegate.
        // If you make a extension method like the below for example, it is available that the compiler infers the type.
        public static class MockRepositoryMixin
        {
            public static Mock<TMock> Create<TZZ, TMock>(this MockRepository repo, TZZ zz, Expression<Func<TZZ, TMock>> indirection) where TMock : class
            {
                var mock = repo.Create<TMock>();
                ((PropertyInfo)((MemberExpression)indirection.Body).Member).SetValue(zz, mock.Object);
                return mock;
            }
        }
    }
}
