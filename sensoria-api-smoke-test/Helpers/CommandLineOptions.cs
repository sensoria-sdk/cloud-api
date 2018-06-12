using System;
using System.Collections.Generic;
using System.Linq;

namespace Sensoria.SmokeTest.Api.Helpers
{
    public class CommandLineOptions
    {
        public string apiUrl;
        public string authUrl;
        public string authorizationEndpoint;
        public string tokenEndpoint;
        public string clientId;
        public string clientSecret;
        public string username;
        public string password;
        public string testOnly;

        public const string SMOKETEST_ACCOUNT = "smoketest@sensoriainc.com";

        private Dictionary<string, string> defaultValues;

        private void InitializeDefaults() 
        {
            defaultValues["environment"] = "prod";
            defaultValues["apiurl"] = "https://api.sensoriafitness.com";
            defaultValues["authurl"] = "https://auth.sensoriafitness.com";
            defaultValues["clientid"] = "***";
            defaultValues["clientsecret"] = "***";
            defaultValues["username"] = SMOKETEST_ACCOUNT;
            defaultValues["password"] = "TestPassword";
            defaultValues["testonly"] = "";
        }


        public CommandLineOptions()
        {
            defaultValues = new Dictionary<string, string>();
            InitializeDefaults();
        }

        public void ProcessCommandArguments(string[] args)
        {
            for (int i = 0; i< args.Count(); i++)
            {
                string optionName = args[i].Substring(1);
                i++;
                string optionValue = args[i];
                defaultValues[optionName.ToLower()] = optionValue;
            }

            SetOptionVariables();
        }

        private void SetOptionVariables()
        {
            apiUrl = defaultValues["apiurl"];
            authUrl = defaultValues["authurl"];
            if (defaultValues["environment"].ToLower().StartsWith("test"))
            {
                apiUrl = "https://test-api.sensoriafitness.com/api/1.0/";
                authUrl = "https://test-auth.sensoriafitness.com/";
            }
            if (defaultValues["environment"].ToLower().StartsWith("prod"))
            {
                apiUrl = "https://api.sensoriafitness.com/api/1.0/";
                authUrl = "https://auth.sensoriafitness.com/";
            }

            authorizationEndpoint = String.Format("{0}oauth20", authUrl);
            tokenEndpoint = String.Format("{0}oauth20/token", authUrl);
            clientId = defaultValues["clientid"];
            clientSecret = defaultValues["clientsecret"];
            username = defaultValues["username"];
            password = defaultValues["password"];
            testOnly = defaultValues["testonly"];
        }
    }
}
