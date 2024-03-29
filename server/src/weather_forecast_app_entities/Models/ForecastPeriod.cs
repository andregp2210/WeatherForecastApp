﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_forecast_app_entities.Models
{
    public class ForecastPeriod
    {
        public int? number { get; set; }
        public string name { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int? temperature { get; set; }
        public string temperatureUnit { get; set; }
        public Precipitation? probabilityOfPrecipitation { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
        public string icon { get; set; }
    }
}
