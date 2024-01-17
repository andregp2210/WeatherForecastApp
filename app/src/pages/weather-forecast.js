import React from "react";
import { ToastContainer } from "react-toastify";
import { ForecastList } from "../containers/forecast-list";
import { WeatherDetail } from "../components/weather-detail";
import { ExtraWeatherInfo } from "../components/extra-weather-info";
import { useWeatherForecastContext } from "../context/weather-forecast";

const WeatherForecast = () => {
  const { serverResponse, selectedForecastDay, setForecastDayById } =
    useWeatherForecastContext();

  if (!serverResponse.data) {
    return (
      <section className="rounded-lg p-12 text-white shadow-xl bg-[#0b131e]">
        <h2 className=" text-5xl font-bold">No data was found</h2>
      </section>
    );
  }

  return (
    <>
      <ToastContainer />
      <section className="rounded-lg p-2 md:p-8 text-white shadow-xl grid grid-col md:grid-cols-[40%_60%] bg-[#0b131e] w-full max-w-7xl">
        <ForecastList
          forecastList={serverResponse.data}
          isLoading={serverResponse.isLoading}
          getForecastDayById={setForecastDayById}
        />
        {selectedForecastDay && (
          <div className="mx-1 md:mx-4 space-y-8">
            <WeatherDetail
              title={selectedForecastDay.name}
              temperature={selectedForecastDay.temperature}
              temperatureUnit={selectedForecastDay.temperatureUnit}
              icon={selectedForecastDay.icon}
            />
            <ExtraWeatherInfo
              detailedForecast={selectedForecastDay.detailedForecast}
              windSpeed={selectedForecastDay.windSpeed}
              windDirection={selectedForecastDay.windDirection}
              probabilityOfPrecipitation={
                selectedForecastDay.probabilityOfPrecipitation
              }
            />
          </div>
        )}
      </section>
    </>
  );
};

export { WeatherForecast };

// if (isLoading) {
//     return (
//       <div role="status" className="max-w-sm animate-pulse space-y-3">
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <div className="rounded-lg p-4  max-w-xs shadow-lg h-36 bg-gray-200" />
//         <span className="sr-only">Loading...</span>
//       </div>
//     );
//   }
