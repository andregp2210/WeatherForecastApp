using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_forecast_app_entities.Models
{
    public class Precipitation
    {
        public string unitCode { get; set; }
        public int? value { get; set; }
    }
}
