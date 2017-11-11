using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace BrightcoveBulkReTranscodeJob
{
    class Program
    {
        const string ClientId = "";
        const string ClientSecret = "";
        const string AccountId = "";

        static void Main(string[] args)
        {
            var videoIds = new Queue<string>(File.ReadLines(@"C:\Users\paul ness\Downloads\brightcove usa.csv"));
            while (videoIds.Count > 0)
            {
                string vidId = videoIds.Peek();
                var formData =
                    string.Format("client_id={1}&client_secret={2}&url=https%3A%2F%2Fingest.api.brightcove.com%2Fv1%2Faccounts%2F{3}%2Fvideos%2F{0}%2Fingest-requests&requestBody=%7B%22master%22%3A%7B%22use_archived_master%22%3A%20true%20%7D%2C%22profile%22%3A%22videocloud-default-v1%22%7D&requestType=POST", vidId, ClientId, ClientSecret, AccountId);
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("https://solutions.brightcove.com/bcls/bcls-proxy/bcls-proxy.php");
                wr.Method = "POST";
                wr.Host = "solutions.brightcove.com";
                wr.KeepAlive = true;
                wr.ContentType = "application/x-www-form-urlencoded";
                wr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 OPR/45.0.2552.898";
                wr.Referer = "http://docs.brightcove.com/en/video-cloud/di-api/samples/di-retranscode.html";
                wr.Headers.Add("Origin", "http://docs.brightcove.com");
                using (StreamWriter sw = new StreamWriter(wr.GetRequestStream()))
                {
                    sw.Write(formData);
                }

                using (var r = new StreamReader(wr.GetResponse().GetResponseStream()))
                {
                    string response = r.ReadToEnd();
                    if (!response.Contains("CONCURRENT") && !response.Contains("MAX") && !response.Contains("RATE") && !response.Contains("LIMIT"))
                    {
                        videoIds.Dequeue();
                        Console.WriteLine(string.Format("Video done. Id: {0}    response: {1}", vidId, response));
                    }
                    else
                    {
                        Console.WriteLine("Error waiting 1 minute to try again: " + response);
                        Thread.Sleep(1000 * 60);
                    }
                }

            }
        }
    }
}
