using Sensoria.SmokeTest.Api.Models;
using System;
using System.Collections.Generic;

namespace Sensoria.SmokeTest.Api.Helpers
{
    public static class DumpHelper
    {
        public static void DumpStringArray(string[] strings)
        {
            Console.Write("[");
            foreach (string s in strings)
            {
                Console.Write(s + ",");
            }

            Console.WriteLine("]");
        }

        public static void DumpUsers(IEnumerable<User> users, int max = 999)
        {
            int count = 0;

            foreach (User u in users)
            {
                if (count > max)
                {
                    break;

                }
                Console.WriteLine("ID: " + u.UserId);
                Console.WriteLine("Display Name: " + u.DisplayName);
                Console.WriteLine("Email: " + u.EmailAddress);
                Console.WriteLine("User Name: " + u.FirstName + " " + u.LastName);

                ++count;
            }
        }
    }
}
