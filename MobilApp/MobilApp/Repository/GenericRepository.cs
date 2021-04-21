using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository.Exceptions;

namespace MobilApp.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private HttpClient httpClient;

        public GenericRepository()
        {
            httpClient = new HttpClient();
        }

        #region GET
        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                ConfigureHttpClient(authToken);

                string jsonResult = string.Empty;

                HttpResponseMessage responseMessage = await httpClient.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion GET

        #region HELPER
        private void ConfigureHttpClient(string authToken)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
        #endregion HELPER
    }
}
