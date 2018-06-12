using DotNetOpenAuth.OAuth2;
using Sensoria.SmokeTest.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensoria.SmokeTest.Api.Tests
{
    public class TestAuthentication
    {
        CommandLineOptions clo;
        public TestAuthentication(CommandLineOptions clo)
        {
            this.clo = clo;
        }

        // returns the authentication token
        public string Run()
        {
            string accessToken = "";
            Console.WriteLine("--- Authorization---");
            Console.WriteLine("Authorization endpoint: " + clo.authorizationEndpoint);
            Console.WriteLine("Token endpoint: " + clo.tokenEndpoint);

            AuthorizationServerDescription authServerDescription = new AuthorizationServerDescription()
            {
                AuthorizationEndpoint = new Uri(clo.authorizationEndpoint),
                TokenEndpoint = new Uri(clo.tokenEndpoint)
            };

            Console.WriteLine("Client ID: " + clo.clientId);
            Console.WriteLine("Client secret: ***");
            UserAgentClient client = new UserAgentClient(authServerDescription, clo.clientId, clo.clientSecret);

            Console.WriteLine("User: " + clo.username);
            Console.WriteLine("Password: ***");
            List<string> scopes = new List<string>();
            scopes.Add("sessions.read");
            scopes.Add("sessions.write");
            scopes.Add("users.read");
            scopes.Add("users.write");
            scopes.Add("shoes.read");
            scopes.Add("shoes.write");
            scopes.Add("shoes.delete");
            scopes.Add("settings.read");
            IAuthorizationState authState = client.ExchangeUserCredentialForToken(clo.username, clo.password, scopes);

            if (authState != null && authState.AccessToken != null)
            {
                accessToken = authState.AccessToken;
                Console.WriteLine("Access token: " + accessToken);
            }

            Console.WriteLine("Success!");
            Console.WriteLine();

            return accessToken;
        }
    }
}
