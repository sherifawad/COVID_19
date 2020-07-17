using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace COVID_19
{
    public class RepositoryApi : IRepositoryApi
    {

        private readonly JsonSerializerSettings _serializerSettings;

        public RepositoryApi()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                string jsonResult = string.Empty;

                //using (HttpClient client = new HttpClient())
                //{
                //    //client.BaseAddress = new Uri(uri);
                //    //client.Timeout = TimeSpan.FromSeconds(900);
                //    //client.DefaultRequestHeaders.Accept.Clear();
                //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    var response = client.GetAsync(uri);
                //    response.Wait();
                //    await HandleResponse(response.Result);

                //    jsonResult = await response.Result.Content.ReadAsStringAsync().ConfigureAwait(false);
                //    var t = JsonConvert.DeserializeObject<T>(jsonResult);
                //    return t;
                //}


                HttpClient httpClient = CreateHttpClient(authToken);

                //string jsonResult = string.Empty;

                var responseMessage =  await  ResponceMessage(uri, httpClient);
                //var responseMessage = await httpClient.GetAsync(uri);

                await HandleResponse(responseMessage);

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                //var json = await Task.Run(() => JsonConvert.DeserializeObject<T>(jsonResult, _serializerSettings));
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

        private static async  Task<HttpResponseMessage> ResponceMessage(string uri, HttpClient httpClient)
        {

            HttpResponseMessage response = null;
            var retryPolicy =  Policy
            //.Handle<WebException>(ex =>
            //{
            //    Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
            //    return true;
            //})
            .Handle<HttpRequestException>()
            .WaitAndRetryAsync
            (
                5,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            );

            await retryPolicy.ExecuteAsync(async () =>
            {
                response = await  httpClient.GetAsync(uri);
                //response.EnsureSuccessStatusCode();
            });

            
            return response;

        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }
    }
}
