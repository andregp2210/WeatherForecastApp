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
        public async Task<LocationResult> getLocationDataAsync(Uri url, string address)
        {
            SetBaseUrl(url);
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "onelineaddress"), $"address={address}&benchmark=2020&format=json");

            return await GetAsync<LocationResult>(requestUrl);
        }

    }
}
