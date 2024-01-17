using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather_forecast_app_entities.Models
{
    public class AddressMatch
    {
        public Coordinates coordinates { get; set; }
        public string matchedAddress { get; set; }
    }
}
