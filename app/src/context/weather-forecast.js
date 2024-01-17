import React, { createContext, useContext, useState } from "react";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

const WeatherForecastContext = createContext();

export const WeatherForecastContextProvider = ({ children }) => {
  const [serverResponse, setServerResponse] = useState({
    isLoading: false,
    data: [
      {
        number: 1,
        name: "Today",
        startTime: "2024-01-16T11:00:00-05:00",
        endTime: "2024-01-16T18:00:00-05:00",
        temperature: 34,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: 70,
        },
        windSpeed: "10 to 18 mph",
        windDirection: "NW",
        shortForecast: "Light Snow Likely",
        detailedForecast:
          "Snow likely before 1pm. Cloudy. High near 34, with temperatures falling to around 28 in the afternoon. Northwest wind 10 to 18 mph, with gusts as high as 29 mph. Chance of precipitation is 70%. Little or no snow accumulation expected. Little or no ice accumulation expected.",
        icon: "https://api.weather.gov/icons/land/day/snow,70?size=medium",
      },
      {
        number: 2,
        name: "Tonight",
        startTime: "2024-01-16T18:00:00-05:00",
        endTime: "2024-01-17T06:00:00-05:00",
        temperature: 14,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: null,
        },
        windSpeed: "8 to 16 mph",
        windDirection: "NW",
        shortForecast: "Mostly Clear",
        detailedForecast:
          "Mostly clear, with a low around 14. Northwest wind 8 to 16 mph, with gusts as high as 23 mph.",
        icon: "https://api.weather.gov/icons/land/night/few?size=medium",
      },
      {
        number: 3,
        name: "Wednesday",
        startTime: "2024-01-17T06:00:00-05:00",
        endTime: "2024-01-17T18:00:00-05:00",
        temperature: 28,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: null,
        },
        windSpeed: "8 to 13 mph",
        windDirection: "W",
        shortForecast: "Sunny",
        detailedForecast:
          "Sunny, with a high near 28. West wind 8 to 13 mph, with gusts as high as 21 mph.",
        icon: "https://api.weather.gov/icons/land/day/skc?size=medium",
      },
      {
        number: 4,
        name: "Wednesday Night",
        startTime: "2024-01-17T18:00:00-05:00",
        endTime: "2024-01-18T06:00:00-05:00",
        temperature: 20,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: null,
        },
        windSpeed: "9 mph",
        windDirection: "SW",
        shortForecast: "Partly Cloudy",
        detailedForecast:
          "Partly cloudy, with a low around 20. Southwest wind around 9 mph.",
        icon: "https://api.weather.gov/icons/land/night/sct?size=medium",
      },
      {
        number: 5,
        name: "Thursday",
        startTime: "2024-01-18T06:00:00-05:00",
        endTime: "2024-01-18T18:00:00-05:00",
        temperature: 37,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: null,
        },
        windSpeed: "7 mph",
        windDirection: "SW",
        shortForecast: "Partly Sunny",
        detailedForecast:
          "Partly sunny, with a high near 37. Southwest wind around 7 mph.",
        icon: "https://api.weather.gov/icons/land/day/bkn?size=medium",
      },
      {
        number: 6,
        name: "Thursday Night",
        startTime: "2024-01-18T18:00:00-05:00",
        endTime: "2024-01-19T06:00:00-05:00",
        temperature: 28,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: 40,
        },
        windSpeed: "8 mph",
        windDirection: "NE",
        shortForecast: "Chance Light Snow",
        detailedForecast:
          "A chance of snow after 7pm. Cloudy, with a low around 28. Chance of precipitation is 40%. Little or no snow accumulation expected.",
        icon: "https://api.weather.gov/icons/land/night/snow,20/snow,40?size=medium",
      },
      {
        number: 7,
        name: "Friday",
        startTime: "2024-01-19T06:00:00-05:00",
        endTime: "2024-01-19T18:00:00-05:00",
        temperature: 33,
        temperatureUnit: "F",
        probabilityOfPrecipitation: {
          unitCode: "wmoUnit:percent",
          value: 60,
        },
        windSpeed: "10 mph",
        windDirection: "N",
        shortForecast: "Light Snow Likely",
        detailedForecast:
          "Snow likely. Mostly cloudy, with a high near 33. Chance of precipitation is 60%.",
        icon: "https://api.weather.gov/icons/land/day/snow,60?size=medium",
      },
    ],
    error: null,
  });
  const [selectedForecastDay, setSelectedForecastDay] = useState(null);
  const navigate = useNavigate();

  const getWeatherForecastData = async ({
    streetName,
    streetNumber,
    state,
    city,
    zipcode,
  }) => {
    setServerResponse((prev) => ({
      ...prev,
      isLoading: true,
    }));
    try {
      const response = await fetch(
        `http://localhost:5240/weatherForecast/get-weather-forecast?address=${streetNumber} ${streetName}, ${city}, ${state} ${zipcode}`
      );
      const result = await response.json();
      setServerResponse((prev) => ({
        ...prev,
        isLoading: false,
        data: result.result,
        error: result.message,
      }));
      if (result.message) {
        toast.error(`Error: ${result.message.description}`);
        return;
      }
      setSelectedForecastDay(result.result[0]);
      navigate("/weather-forecast");
    } catch (error) {
      setServerResponse((prev) => ({
        ...prev,
        isLoading: false,
        error,
      }));
      toast.error("Ups, something wrong happened.");
    }
  };

  const setForecastDayById = (id) => {
    const forecastDay = serverResponse.data.find(({ number }) => number === id);
    if (forecastDay) {
      setSelectedForecastDay(forecastDay);
    }
  };

  return (
    <WeatherForecastContext.Provider
      value={{
        serverResponse,
        selectedForecastDay,
        getWeatherForecastData,
        setForecastDayById,
      }}
    >
      {children}
    </WeatherForecastContext.Provider>
  );
};

export const useWeatherForecastContext = () => {
  return useContext(WeatherForecastContext);
};
