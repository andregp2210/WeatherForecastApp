import React from "react";

const WeatherCard = ({
  title,
  temperature,
  temperatureUnit,
  icon,
  shortForecast,
  id,
  getForecastDayById,
}) => (
  <article
    onClick={() => getForecastDayById(id)}
    className="w-100 cursor-pointer p-1 md:p-4 text-white max-w-sm grid  grid-cols-[25%_20%_30%_25%] border-b border-[#35455e] hover:text-opacity-80 hover:border-white"
  >
    <p className="justify-self-start">{title}</p>
    <figure className="justify-self-end">
      <img
        src={icon}
        alt="weater icon"
        className="rounded-lg"
        height={"30px"}
        width={"30px"}
      />
    </figure>
    <p className="text-sm pl-4 justify-self-start">{shortForecast}</p>
    <p className="text-xl justify-self-end font-bold mt-0 md:mt-1">
      {`${temperature} ${temperatureUnit}`}
    </p>
  </article>
);

export { WeatherCard };
