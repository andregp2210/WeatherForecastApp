using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_forecast_app_entities.Models
{
    public class Point
    {
        public int gridX { get; set; }
        public int gridY { get; set; }
        public string? forecast { get; set; }
    }
}
