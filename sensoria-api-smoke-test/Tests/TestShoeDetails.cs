using Sensoria.Api.Core.Models;
using Sensoria.SmokeTest.Api.Helpers;
using Sensoria.SmokeTest.Api.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Sensoria.SmokeTest.Api.Tests
{
    public class TestShoeDetails
    {
        CommandLineOptions clo;
        string accessToken;
        User user = null;
        public bool ShowHeaders = false;
        public string Url = "";
        public string AccessToken = "";
        public string BaseAddress = "";

        public TestShoeDetails(CommandLineOptions clo, string accessToken, User user)
        {
            this.clo = clo;
            this.accessToken = accessToken;
            this.user = user;
        }

        public void Run()
        {
            //Get the list of shoes in the user Shoe Closet
            Console.WriteLine("****** SHOE DETAILS TEST ******");

            SensoriaApiResult<ProductDetailsSearchItem> resultGetShoeDetails = GetShoeDetails(1);
            Trace.Assert(resultGetShoeDetails.IsSuccess == true);
            Trace.Assert(resultGetShoeDetails.APIResult != null);
            Trace.Assert(resultGetShoeDetails.APIResult.Id != null);
            Trace.Assert(resultGetShoeDetails.APIResult.Name != null);
            Trace.Assert(resultGetShoeDetails.APIResult.BrandName != null);

            Console.WriteLine("Get Shoe Details correctly done");
        }

        #region ShoeDetails Methods
        private SensoriaApiResult<ProductDetailsSearchItem> GetShoeDetails(int ProductId)
        {
            ApiResultHttpClientHelper<ProductDetailsSearchItem> client = new ApiResultHttpClientHelper<ProductDetailsSearchItem>();
            client.Url = String.Format("search/product/details/{0}", ProductId);
            client.AccessToken = accessToken;
            client.BaseAddress = clo.apiUrl;

            return client.GetSensoriaApiResult(); 
        }
        #endregion
    }
}
