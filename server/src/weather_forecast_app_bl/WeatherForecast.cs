using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using weather_forecast_app_dac.Services;
using weather_forecast_app_entities.Models;

namespace weather_forecast_app_bl
{
    public class WeatherForecast
    {
        private readonly Uri locationUrl = new Uri("https://geocoding.geo.census.gov/geocoder/locations/");
        private readonly Uri pointUrl = new Uri("https://api.weather.gov/");
        private readonly IApiClient apiClient;
        private List<CustomApiHeader> customHeaders = new List<CustomApiHeader> { new CustomApiHeader { key = "User-Agent", value = "forecastapp.com" } };

        public WeatherForecast(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<ObjResult> getWeatherForecast(string address)
        {
            ObjResult result = new ObjResult();
            List<ForecastPeriod> forecasts = new List<ForecastPeriod>();

            try
            {
                var locationFactoryResult = await apiClient.getLocationDataAsync(locationUrl, address);
                if (locationFactoryResult.result.addressMatches.Count == 0)
                {
                    result.Message = new InfoMessage("404", "ERROR", "Unfortunately, we couldn't locate the provided address.");
                    return result;
                }

                Coordinates coordinates = locationFactoryResult.result.addressMatches[0].coordinates;
                var pointFactoryResult = await apiClient.getPointDataAsync(pointUrl, coordinates, customHeaders);
                if (String.IsNullOrEmpty(pointFactoryResult.properties.forecast))
                {
                    result.Message = new InfoMessage("1", "ERROR", "Forecast url couldn't be null.");
                    return result;
                }
                Uri weatherForecastUrl = new Uri(pointFactoryResult.properties.forecast);
                WeatherForecastResult weatherForecastResult = await apiClient.getForecastDataAsync(weatherForecastUrl, customHeaders);
                if (weatherForecastResult.properties.periods.Count == 0)
                {
                    result.Message = new InfoMessage("404", "ERROR", "Unfortunately, we couldn't obtain weather data for the provided address.");
                    return result;
                }

                for (int i = 0; i < 7; i++)
                {
                    forecasts.Add(weatherForecastResult.properties.periods[i]);
                }
                result.Result = forecasts;

            }
            catch (Exception ex)
            {
                result.Message = new InfoMessage("1", "ERROR", ex.Message);
            }
            return result;
        }
    }
}
