import React, { createContext, useContext, useState } from "react";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

const BASE_URL = process.env.REACT_APP_API_URL;

const WeatherForecastContext = createContext();

export const WeatherForecastContextProvider = ({ children }) => {
  const [serverResponse, setServerResponse] = useState({
    isLoading: false,
    data: null,
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
        `${BASE_URL}weatherForecast/get-weather-forecast?address=${streetNumber} ${streetName}, ${city}, ${state} ${zipcode}`
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
