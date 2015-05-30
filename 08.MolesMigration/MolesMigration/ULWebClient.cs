using System;
using System.Net;

namespace MolesMigration
{
    public class ULWebClient
    {
        public static void ShowGoogle()
        {
            var client = new WebClient();
            client.DownloadStringCompleted += (sender, e) =>
            {
                Console.WriteLine(e.Result);
            };
            client.DownloadStringAsync(new Uri("http://google.co.jp/"));
        }
    }
}
