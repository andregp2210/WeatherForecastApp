import React from "react";
import { Route, Routes as Switch } from "react-router-dom";


import { WeatherForecast } from "../pages/weather-forecast";
import { UserAddress } from "../pages/user-address";

const NotFound = () => {
  return (
    <div>
      <h2>404 - Not Found</h2>
      <p>Oops! The page you're looking for doesn't exist.</p>
    </div>
  );
};

const Routes = () => {
  return (
    <Switch>
      <Route path="/" element={<UserAddress />} />
      <Route path="/weather-forecast" element={<WeatherForecast />} />
      <Route path="*" element={<NotFound />} />
    </Switch>
  );
};

export default Routes;
