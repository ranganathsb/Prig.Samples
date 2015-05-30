using System.Net;

namespace FakesMigration
{
    public class WebServiceClient
    {
        public bool CallWebService(string url)
        {
            var request = CreateWebRequest(url);
            var isValid = true;
            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                isValid = HttpStatusCode.OK == response.StatusCode;
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        static HttpWebRequest CreateWebRequest(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Method = "GET";
            request.Timeout = 1000;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            return request;
        }
    }
}
