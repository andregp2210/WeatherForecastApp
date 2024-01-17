using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using weather_forecast_app_entities.Models;

namespace weather_forecast_app_dac.Services
{
    public partial class ApiClient: IApiClient
    {
        private HttpClient _httpClient;
        private Uri BaseEndpoint { get; set; }
        public ApiClient()
        {

        }
        private void SetBaseUrl(Uri url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("BaseEndpoint");
            }
            BaseEndpoint = url;
        }

        private Uri CreateRequestUri(string? url = "", string? queryString = "", List<CustomApiHeader>? customHeaders = null)
        {
            _httpClient = new HttpClient();
            var endpoint = new Uri(BaseEndpoint, url);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            if (customHeaders != null)
            {
                AddHeaders(customHeaders);
            }
            return uriBuilder.Uri;
        }
        private void AddHeaders(List<CustomApiHeader> headers)
        {
            headers.ForEach((header) => { _httpClient.DefaultRequestHeaders.Add(header.key, header.value); });
        }
        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data)!;
        }

    }
}
