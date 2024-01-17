import React from "react";

import { WeatherCard } from "../components/weather-card";

const ForecastList = ({ forecastList, getForecastDayById }) => (
  <section className="space-y-3 p-2 md:p-5 bg-[#202b3b] rounded-lg">
    <p>7 - Day forecast</p>
    {forecastList &&
      forecastList.map(
        ({
          name,
          number,
          temperature,
          temperatureUnit,
          icon,
          shortForecast,
        }) => (
          <WeatherCard
            key={`${name}-${number}`}
            title={name}
            id={number}
            temperature={temperature}
            temperatureUnit={temperatureUnit}
            icon={icon}
            shortForecast={shortForecast}
            getForecastDayById={getForecastDayById}
          />
        )
      )}
  </section>
)

export { ForecastList };
