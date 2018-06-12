using Sensoria.SmokeTest.Api.Helpers;
using Sensoria.SmokeTest.Api.Models;
using Sensoria.SmokeTest.Api.Tests;
using System;
using System.Net;

namespace Sensoria.SmokeTest.Api
{
    class Program
    {
        static CommandLineOptions clo;
        static string accessToken;
        static User user;
        static void Main(string[] args)
        {
            ServicePointManager
                .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;

            clo = new CommandLineOptions();
            clo.ProcessCommandArguments(args);


            accessToken = new TestAuthentication(clo).Run();
            user = new TestUserAccess(clo, accessToken).Run();

            if (!ShouldTestOnly("Access"))
            {
                if (ShouldTest("Sessions"))
                    new TestSession(clo, accessToken, user).Run();

                if (ShouldTest("ShoeCloset"))
                    new TestShoeCloset(clo, accessToken, user).Run();

                if (ShouldTest("Achievements"))
                    new TestAchievement(clo, accessToken, user).Run();

                if (ShouldTest("ShoeDetails"))
                    new TestShoeDetails(clo, accessToken, user).Run();
            }
           
            Console.Write("Test complete!");
        }

        private static bool ShouldTest(string areaToTest)
        {
            return (string.IsNullOrWhiteSpace(clo.testOnly) || (string.Compare(clo.testOnly, areaToTest, true) == 0));
        }

        private static bool ShouldTestOnly(string areaToTest)
        {
            return (string.Compare(clo.testOnly, areaToTest, true) == 0);
        }
    }
}
