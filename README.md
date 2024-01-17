# Weather Forecast App

Welcome to the Weather Forecast App! This application provides weather forecasts based on the provided address.

## Getting Started

To run the project, you need to update two configuration files. Follow the steps below:

### 1. `appsettings.Development.json`

Navigate to `server/src/weather_forecast_app_client/appsettings.Development.json`. This file contains configuration settings for the server.

```json
{
    "locationApiUrl": "https://geocoding.geo.census.gov/geocoder/locations/",
    "pointApiUrl": "https://api.weather.gov/"
}
```

After updating the `appsettings.Development.json`, open `/server/src/weather_forecast_app_client/weather_forecast_app_client.sln` in Visual Studio and run the application.

### 2. `.env` File

Navigate to the `app` folder. Here, you will find a file named `.env.example`. Copy the variable names from this file and create a new file named `.env` in the same folder.


Example content of `.env.example`:

makefileCopy code

`REACT_APP_API_URL=your_api_url`

Replace `your_api_url` with your local API url.

Now, rename `.env.example` to `.env`.

## Running the Application

With the configuration files updated, you are ready to run the Weather Forecast App. Follow the steps provided earlier to run the server and make sure to have the necessary API keys for map and weather services.

Open your browser and go to `http://localhost:3000/` to access the Weather Forecast App.

Enjoy exploring the weather forecasts!
