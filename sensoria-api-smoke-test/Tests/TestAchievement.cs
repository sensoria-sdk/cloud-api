using Sensoria.SmokeTest.Api.Helpers;
using Sensoria.SmokeTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sensoria.SmokeTest.Api.Tests
{
    public class TestAchievement
    {
        CommandLineOptions clo;
        string accessToken;
        User user;

        public TestAchievement(CommandLineOptions clo, string accessToken, User user)
        {
            this.clo = clo;
            this.accessToken = accessToken;
            this.user = user;
        }

        public void Run()
        {
            Console.WriteLine("*****START ACHIEVEMENTS TEST*****");

            Console.WriteLine("Get all the badges (achieved/unachieved) for user");
            SensoriaApiResult<IEnumerable<Achievement>> resultGetBadges = GetBadges(user.UserId);
            Trace.Assert(resultGetBadges.StatusCode == System.Net.HttpStatusCode.OK, "FAILURE: No badges exist");
            Trace.Assert(resultGetBadges.APIResult.Count() > 0);
            Console.WriteLine("Badges Exists - Correct");

            Console.WriteLine("Get all the PRs (achieved/unachieved) for user");
            SensoriaApiResult<IEnumerable<Achievement>> resultPersonalRecords = GetPersonalRecords(user.UserId);
            Trace.Assert(resultPersonalRecords.StatusCode == System.Net.HttpStatusCode.OK, "FAILURE: No Personal Records exists");
            Trace.Assert(resultPersonalRecords.APIResult.Count() > 0);
            Console.WriteLine("Personal Records Exixts - Correct");

            Console.WriteLine("Get all achieved most recent badges for user");
            SensoriaApiResult<IEnumerable<Achievement>> recentBadges = GetRecentBadges(user.UserId);
            Trace.Assert(recentBadges.StatusCode == System.Net.HttpStatusCode.OK, "FAILURE: No badges exist");
            Trace.Assert(recentBadges.APIResult.Count() > 0);
            Console.WriteLine("Recent Badges Fetched - Correct");
        }

        #region Get Achievement Methods

        private SensoriaApiResult<IEnumerable<Achievement>> GetBadges(int id)
        {
            ApiResultHttpClientHelper<IEnumerable<Achievement>> client = new ApiResultHttpClientHelper<IEnumerable<Achievement>>();

            client.Url = String.Format("user/{0}/achievement?inquiry=Badge", id);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.GetSensoriaApiResult();
        }

        private SensoriaApiResult<IEnumerable<Achievement>> GetPersonalRecords(int id)
        {
            ApiResultHttpClientHelper<IEnumerable<Achievement>> client = new ApiResultHttpClientHelper<IEnumerable<Achievement>>();

            client.Url = String.Format("user/{0}/achievement?inquiry=PersonalRecord", id);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.GetSensoriaApiResult();
        }

        private SensoriaApiResult<IEnumerable<Achievement>> GetRecentBadges(int id)
        {
            ApiResultHttpClientHelper<IEnumerable<Achievement>> client = new ApiResultHttpClientHelper<IEnumerable<Achievement>>();

            client.Url = String.Format("user/{0}/achievement/recentbadges", id);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.GetSensoriaApiResult();
        }

        #endregion
    }
}
