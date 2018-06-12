using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sensoria.SmokeTest.Api.Helpers;
using Sensoria.SmokeTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensoria.SmokeTest.Api.Tests
{
    public class TestSession
    {
        CommandLineOptions clo;
        string accessToken;
        User user;

        public TestSession(CommandLineOptions clo, string accessToken, User user)
        {
            this.clo = clo;
            this.accessToken = accessToken;
            this.user = user;
        }

        public void Run()
        {
            // Enumerating Sessions
            ApiResultHttpClientHelper<IEnumerable<JObject>> client = new ApiResultHttpClientHelper<IEnumerable<JObject>>();
            client.Url = String.Format("{0}User/me/Session", clo.apiUrl);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            var result = client.GetSensoriaApiResult().APIResult;
            var count = result.Count();
            Console.WriteLine(String.Format("Available {0} sessions, enumerating top 10", count));

            int i = 0;
            foreach (JObject session in result)
            {
                if (i >= 10)
                {
                    break;
                }

                Console.WriteLine("Fetching session {0}", session["SessionId"]);

                ApiResultHttpClientHelper<JObject> sessionClient = new ApiResultHttpClientHelper<JObject>();
                sessionClient.Url = String.Format("{0}Session/{1}", clo.apiUrl, session["SessionId"]);
                sessionClient.AccessToken = accessToken;
                sessionClient.BaseAddress = clo.apiUrl;

                var sessionData = sessionClient.GetSensoriaApiResult().APIResult;
                Console.WriteLine("Session {0} fetched, recorded on {1}", sessionData["SessionId"], sessionData["RecordedDateTime"]);

                ++i;
            }
        }
    }
}
