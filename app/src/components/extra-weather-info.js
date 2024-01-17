import React from "react";

const SYMBOLS = {
  "wmoUnit:percent": "%",
};

const ExtraWeatherInfo = ({
  windSpeed,
  windDirection,
  probabilityOfPrecipitation,
  detailedForecast,
}) => (
  <div className="my-4 p-3 md:p-5 text-white bg-[#202b3b] rounded-lg">
    <p className="text-lg">{detailedForecast}</p>
    <br />
    <p className="text-xl font-bold">Air conditions: </p>
    <p className="text-lg">{`speed: ${windSpeed} km/h (${windDirection})`}</p>
    <br />
    <p className="text-xl font-bold">Chance of rain: </p>
    <p className="text-lg">
      {`${probabilityOfPrecipitation.value || 0}${
        SYMBOLS[probabilityOfPrecipitation.unitCode] || ""
      }`}
    </p>
  </div>
);

export { ExtraWeatherInfo };
