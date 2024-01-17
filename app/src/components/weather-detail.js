import React from "react";

const WeatherDetail = ({ title, temperature, temperatureUnit, icon }) => (
  <article className=" text-white">
    <div className="flex justify-between my-5">
      <p className="flex flex-col justify-between">
        <span className="text-lg md:text-xl font-bold">{title}</span>
        <span className="text-5xl md:text-7xl font-bold mt-1">{`${temperature} ${temperatureUnit}`}</span>
      </p>
      <figure className="">
        <img
          src={icon}
          alt="weater icon"
          className="rounded-lg"
          height={"120px"}
          width={"120px"}
        />
      </figure>
    </div>
  </article>
);

export { WeatherDetail };
