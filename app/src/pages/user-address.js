import React from "react";
import { ToastContainer } from "react-toastify";

import { AddressForm } from "../components/address-form";
import { useWeatherForecastContext } from "../context/weather-forecast";
import { STATES_LIST } from "../utils/states";


const UserAddress = () => {
  const { serverResponse, getWeatherForecastData } =
    useWeatherForecastContext();

  const onSubmitForm = ({ formData }) => {
    const {
      streetName,
      streetNumber,
      state,
      city = null,
      zipcode = null,
    } = formData;
    getWeatherForecastData({
      streetName,
      streetNumber,
      state,
      city,
      zipcode,
    });
  };

  return (
    <>
      <ToastContainer />
      <div className="px-5 py-16 bg-[#0b131e] rounded-lg text-white relative w-full max-w-5xl">
        <AddressForm
          statesList={STATES_LIST}
          onSubmitForm={onSubmitForm}
          isLoading={serverResponse.isLoading}
        />
        {serverResponse.isLoading && (
          <div className="backdrop-blur-sm absolute inset-0 flex justify-center items-center">
            <div
              className="w-16 h-16 rounded-full animate-spin
                    border-8 border-solid border-blue-500 border-t-transparent"
            ></div>
          </div>
        )}
      </div>
    </>
  );
};

export { UserAddress };
