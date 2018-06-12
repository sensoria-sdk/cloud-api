using Sensoria.SmokeTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sensoria.SmokeTest.Api.Helpers
{
    public class ApiResultHttpClientHelper<T>
    {
        public string Url = "";
        public string AccessToken = "";
        public string BaseAddress = "";

        public HttpClient CreateClient()
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrWhiteSpace(AccessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }

            return client;
        }

        #region Legacy methods
        // Note: some API methods are not in a SensoriaApiResult envelope, for historical reasons - separate method for those
        public T GetApiResult()
        {
            HttpClient client = CreateClient();

            HttpResponseMessage response;
            response = client.GetAsync(Url).Result;

            T result = response.Content.ReadAsAsync<T>().Result;
            return result;
        }

        public SensoriaApiResult<T> GetSensoriaApiResult()
        {
            HttpClient client = CreateClient();

            HttpResponseMessage response;
            response = client.GetAsync(Url).Result;

            SensoriaApiResult<T> result = response.Content.ReadAsAsync<SensoriaApiResult<T>>().Result;
            return result;
        }
        #endregion

        public async Task<T> GetApiResultAsync()
        {
            using (HttpClient client = CreateClient())
            using (HttpResponseMessage response = await client.GetAsync(Url))
            using (HttpContent content = response.Content)
            {
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public async Task<SensoriaApiResult<T>> GetSensoriaApiResultAsync()
        {
            using (HttpClient client = CreateClient())
            using (HttpResponseMessage response = await client.GetAsync(Url))
            using (HttpContent content = response.Content)
            {
                return await response.Content.ReadAsAsync<SensoriaApiResult<T>>();
            }
        }

        public SensoriaApiResult<T> PostSensoriaApiResult(object body)
        {
            HttpClient client = CreateClient();

            HttpResponseMessage response = client.PostAsJsonAsync(Url, body).Result;
            SensoriaApiResult<T> result = response.Content.ReadAsAsync<SensoriaApiResult<T>>().Result;
            return result;
        }

        public SensoriaApiResult<T> PutSensoriaApiResult(object body)
        {
            HttpClient client = CreateClient();

            HttpResponseMessage response;
            response = client.PutAsJsonAsync(Url, body).Result;

            SensoriaApiResult<T> result = response.Content.ReadAsAsync<SensoriaApiResult<T>>().Result;
            return result;
        }

        public SensoriaApiResult<T> DeleteSensoriaApiResult()
        {
            HttpClient client = CreateClient();

            HttpResponseMessage response;
            response = client.DeleteAsync(Url).Result;

            SensoriaApiResult<T> result = response.Content.ReadAsAsync<SensoriaApiResult<T>>().Result;
            return result;
        }
    }
}

