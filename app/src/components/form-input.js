import React from "react";

const FormInput = ({
  id,
  name,
  label,
  error,
  register,
  placeholder,
  type = "text",
}) => (
  <div className="mb-5">
    <label
      htmlFor={name}
      className="block mb-2 text-sm font-medium text-white"
    >
      {label}:
    </label>
    <input
      type={type}
      id={id}
      className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
      placeholder={placeholder}
      {...register(name)}
    />
    {error[name]?.message && (
      <p className="mt-2 text-sm text-red-500">
        <span className="font-medium">{error[name].message}</span>
      </p>
    )}
  </div>
);

export { FormInput };
