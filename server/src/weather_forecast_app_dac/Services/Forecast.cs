using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using weather_forecast_app_entities.Models;

namespace weather_forecast_app_dac.Services
{
    public partial class ApiClient
    {

        public async Task<WeatherForecastResult> getForecastDataAsync(Uri url, List<CustomApiHeader>? customHeaders = null)
        {
            SetBaseUrl(url);
            var requestUrl = CreateRequestUri(null, null, customHeaders);

            return await GetAsync<WeatherForecastResult>(requestUrl);
        }
        /*------------------------------------*/

    }
}
