import React from "react";
import "./styles/tailwind.css";

import Routes from "./routes/routes";
import { WeatherForecastContextProvider } from "./context/weather-forecast";

const App = () => {
  return (
    <WeatherForecastContextProvider>
      <section className="p-1 md:p-5 w-full md:h-screen flex justify-center items-center bg-gray-400">
        <Routes />
      </section>
    </WeatherForecastContextProvider>
  );
};

export { App };
