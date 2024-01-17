using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather_forecast_app_entities.Models;

namespace weather_forecast_app_dac.Services
{
    public interface IApiClient
    {
        Task<LocationResult> getLocationDataAsync(Uri url, string address);
        Task<WeatherForecastResult> getForecastDataAsync(Uri url, List<CustomApiHeader>? customHeaders = null);
        Task<WeatherPointResult> getPointDataAsync(Uri url, Coordinates coordinates, List<CustomApiHeader>? customHeaders = null);
    }
}
