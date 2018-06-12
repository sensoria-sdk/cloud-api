using Newtonsoft.Json.Linq;
using Sensoria.SmokeTest.Api.Helpers;
using Sensoria.SmokeTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sensoria.SmokeTest.Api.Tests
{
    public class TestUserAccess
    {
        CommandLineOptions clo;
        string accessToken;

        public TestUserAccess(CommandLineOptions clo, string accessToken)
        {
            this.clo = clo;
            this.accessToken = accessToken;
        }

        public User Run()
        {
            User user = null;

            bool singleUser = false;
            try
            {
                user = AccessCurrentUser();

                IEnumerable<User> users = ListUsers();
                ListSessionsForUsers(users, 10);
            }
            catch (Exception)
            {
                Console.WriteLine("No access to users.");
                singleUser = true;
            }

            if (singleUser)
            {
                ListSessionsForMe();
            }
            return user;
        }


        User AccessCurrentUser()
        {
            Console.WriteLine("---Current User---");
            ApiResultHttpClientHelper<User> client = new ApiResultHttpClientHelper<User>();
            client.Url = String.Format("{0}me", clo.apiUrl);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            var user = client.GetApiResult();
            DumpHelper.DumpUsers(new List<User> { user });            
            return user;
        }

        IEnumerable<User> ListUsers()
        {
            Console.WriteLine("---List Users---");
            ApiResultHttpClientHelper <IEnumerable<User>> client = new ApiResultHttpClientHelper<IEnumerable<User>>();
            client.Url = String.Format("{0}User", clo.apiUrl);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            var users = client.GetApiResult();
            DumpHelper.DumpUsers(users, 10);
            return users;
        }

        void ListSessionsForMe()
        {
            // Note: Session is a complex object, only extracting as a dynamic JSON here
            ApiResultHttpClientHelper<IEnumerable<JObject>> client = new ApiResultHttpClientHelper<IEnumerable<JObject>>();
            client.Url = String.Format("{0}User/me/Session", clo.apiUrl);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            var result = client.GetSensoriaApiResult().APIResult;
            Console.WriteLine(String.Format("Available {0} sessions.", result.Count()));
        }

        void ListSessionForUser(User u)
        {
            // Note: Session is a complex object, only extracting as a dynamic JSON here
            ApiResultHttpClientHelper<IEnumerable<JObject>> client = new ApiResultHttpClientHelper<IEnumerable<JObject>>();
            client.Url = String.Format("{0}User/{1}/Session", clo.apiUrl, u.UserId);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            var result = client.GetSensoriaApiResult().APIResult;
            Console.WriteLine(String.Format("Available {0} sessions.", result.Count()));
        }

        void ListSessionsForUsers(IEnumerable<User> users, int max = 999)
        {
            int count = 0;
            foreach (User u in users)
            {
                if (count > max)
                {
                    break;
                }
                ListSessionForUser(u);

                ++count;
            }
        }
    }
}
