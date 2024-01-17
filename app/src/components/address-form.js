import React from "react";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";

import { FormInput } from "./form-input";
import { FormSelect } from "./form-select";

const REQUIRED_OPTION_MESSAGE = "This field is required";

const REGIONS_SCHEMA = yup.object().shape({
  streetName: yup.string().required(REQUIRED_OPTION_MESSAGE),
  streetNumber: yup.string().required(REQUIRED_OPTION_MESSAGE),
  state: yup.string(),
  city: yup.string(),
  zipcode: yup.string(),
});

const AddressForm = ({ statesList, onSubmitForm, isLoading }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    mode: "onSubmit",
    reValidateMode: "onChange",
    resolver: yupResolver(REGIONS_SCHEMA),
  });

  const onSubmit = (data) => onSubmitForm({ formData: data });

  return (
    <form
      className="max-w-sm mx-auto bg-[#202b3b] rounded-lg p-5"
      onSubmit={handleSubmit(onSubmit)}
    >
      <FormInput
        id="streetName"
        name="streetName"
        label="Street Name"
        placeholder="Silver Hill Rd"
        error={errors}
        register={register}
      />
      <FormInput
        id="streetNumber"
        name="streetNumber"
        label="Street Number"
        error={errors}
        register={register}
        placeholder="4600"
      />
      <FormSelect
        id="state"
        name="state"
        label="State"
        options={statesList}
        error={errors}
        register={register}
      />
      <FormInput
        id="city"
        name="city"
        label="City"
        error={errors}
        register={register}
        placeholder="DC"
      />
      <FormInput
        id="zipCode"
        name="zipCode"
        label="Zip code"
        error={errors}
        register={register}
        placeholder="20233"
      />

      <button
        type="submit"
        disabled={isLoading}
        className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 disabled:opacity-50"
      >
        Submit
      </button>
    </form>
  );
};

export { AddressForm };
