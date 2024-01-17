using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using weather_forecast_app_bl;
using weather_forecast_app_dac.Services;
using weather_forecast_app_entities.Models;

namespace weather_forecast_app_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet("get-weather-forecast")]
        public async Task<IActionResult> Get([FromQuery] WeatherForecastParameters weatherForecastParameters)
        {
            IApiClient apiClient = new ApiClient();
            WeatherForecast forecast = new WeatherForecast(apiClient);

            ObjResult result = await forecast.getWeatherForecast(weatherForecastParameters.address);

            if (result.Message != null)
            {
                if (result.Message.Id == "404")
                {
                    return NotFound(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            return Ok(result);

        }
    }
}
