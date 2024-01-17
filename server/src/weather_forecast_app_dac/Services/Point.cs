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

        public async Task<WeatherPointResult> getPointDataAsync(Uri url, Coordinates coordinates, List<CustomApiHeader>? customHeaders = null)
        {
            SetBaseUrl(url);
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                $"points/{coordinates.y},{coordinates.x}"), null, customHeaders);

            return await GetAsync<WeatherPointResult>(requestUrl);
        }
        /*------------------------------------*/

    }
}
