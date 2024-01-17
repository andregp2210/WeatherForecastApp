using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using weather_forecast_app_bl;
using weather_forecast_app_dac.Services;
using weather_forecast_app_entities.Models;
using Xunit;

namespace weather_forecast_app_test
{

    public class WeatherForecastTests
    {
        string locationResponse = "{\r\n  \"result\": {\r\n    \"input\": {\r\n      \"address\": {\r\n        \"address\": \"4600 Silver Hill Rd, Washington, DC 20233\"\r\n      },\r\n      \"benchmark\": {\r\n        \"isDefault\": false,\r\n        \"benchmarkDescription\": \"Public Address Ranges - Census 2020 Benchmark\",\r\n        \"id\": \"2020\",\r\n        \"benchmarkName\": \"Public_AR_Census2020\"\r\n      }\r\n    },\r\n    \"addressMatches\": [\r\n      {\r\n        \"tigerLine\": {\r\n          \"side\": \"L\",\r\n          \"tigerLineId\": \"76355984\"\r\n        },\r\n        \"coordinates\": {\r\n          \"x\": -76.92748724230096,\r\n          \"y\": 38.84601622386617\r\n        },\r\n        \"addressComponents\": {\r\n          \"zip\": \"20233\",\r\n          \"streetName\": \"SILVER HILL\",\r\n          \"preType\": \"\",\r\n          \"city\": \"WASHINGTON\",\r\n          \"preDirection\": \"\",\r\n          \"suffixDirection\": \"\",\r\n          \"fromAddress\": \"4600\",\r\n          \"state\": \"DC\",\r\n          \"suffixType\": \"RD\",\r\n          \"toAddress\": \"4700\",\r\n          \"suffixQualifier\": \"\",\r\n          \"preQualifier\": \"\"\r\n        },\r\n        \"matchedAddress\": \"4600 SILVER HILL RD, WASHINGTON, DC, 20233\"\r\n      }\r\n    ]\r\n  }\r\n}";
        string pointsResponse = "{\r\n  \"@context\": [\r\n    \"https://geojson.org/geojson-ld/geojson-context.jsonld\",\r\n    {\r\n      \"@version\": \"1.1\",\r\n      \"wx\": \"https://api.weather.gov/ontology#\",\r\n      \"s\": \"https://schema.org/\",\r\n      \"geo\": \"http://www.opengis.net/ont/geosparql#\",\r\n      \"unit\": \"http://codes.wmo.int/common/unit/\",\r\n      \"@vocab\": \"https://api.weather.gov/ontology#\",\r\n      \"geometry\": {\r\n        \"@id\": \"s:GeoCoordinates\",\r\n        \"@type\": \"geo:wktLiteral\"\r\n      },\r\n      \"city\": \"s:addressLocality\",\r\n      \"state\": \"s:addressRegion\",\r\n      \"distance\": {\r\n        \"@id\": \"s:Distance\",\r\n        \"@type\": \"s:QuantitativeValue\"\r\n      },\r\n      \"bearing\": {\r\n        \"@type\": \"s:QuantitativeValue\"\r\n      },\r\n      \"value\": {\r\n        \"@id\": \"s:value\"\r\n      },\r\n      \"unitCode\": {\r\n        \"@id\": \"s:unitCode\",\r\n        \"@type\": \"@id\"\r\n      },\r\n      \"forecastOffice\": {\r\n        \"@type\": \"@id\"\r\n      },\r\n      \"forecastGridData\": {\r\n        \"@type\": \"@id\"\r\n      },\r\n      \"publicZone\": {\r\n        \"@type\": \"@id\"\r\n      },\r\n      \"county\": {\r\n        \"@type\": \"@id\"\r\n      }\r\n    }\r\n  ],\r\n  \"id\": \"https://api.weather.gov/points/38.8459999,-76.9275\",\r\n  \"type\": \"Feature\",\r\n  \"geometry\": {\r\n    \"type\": \"Point\",\r\n    \"coordinates\": [\r\n      -76.927499999999995,\r\n      38.845999900000002\r\n    ]\r\n  },\r\n  \"properties\": {\r\n    \"@id\": \"https://api.weather.gov/points/38.8459999,-76.9275\",\r\n    \"@type\": \"wx:Point\",\r\n    \"cwa\": \"LWX\",\r\n    \"forecastOffice\": \"https://api.weather.gov/offices/LWX\",\r\n    \"gridId\": \"LWX\",\r\n    \"gridX\": 101,\r\n    \"gridY\": 70,\r\n    \"forecast\": \"https://api.weather.gov/gridpoints/LWX/101,70/forecast\",\r\n    \"forecastHourly\": \"https://api.weather.gov/gridpoints/LWX/101,70/forecast/hourly\",\r\n    \"forecastGridData\": \"https://api.weather.gov/gridpoints/LWX/101,70\",\r\n    \"observationStations\": \"https://api.weather.gov/gridpoints/LWX/101,70/stations\",\r\n    \"relativeLocation\": {\r\n      \"type\": \"Feature\",\r\n      \"geometry\": {\r\n        \"type\": \"Point\",\r\n        \"coordinates\": [\r\n          -76.922520000000006,\r\n          38.848615000000002\r\n        ]\r\n      },\r\n      \"properties\": {\r\n        \"city\": \"Suitland\",\r\n        \"state\": \"MD\",\r\n        \"distance\": {\r\n          \"unitCode\": \"wmoUnit:m\",\r\n          \"value\": 520.14709395636999\r\n        },\r\n        \"bearing\": {\r\n          \"unitCode\": \"wmoUnit:degree_(angle)\",\r\n          \"value\": 236\r\n        }\r\n      }\r\n    },\r\n    \"forecastZone\": \"https://api.weather.gov/zones/forecast/MDZ013\",\r\n    \"county\": \"https://api.weather.gov/zones/county/MDC033\",\r\n    \"fireWeatherZone\": \"https://api.weather.gov/zones/fire/MDZ013\",\r\n    \"timeZone\": \"America/New_York\",\r\n    \"radarStation\": \"KLWX\"\r\n  }\r\n}";
        string forecastResponse = "{\r\n  \"@context\": [\r\n    \"https://geojson.org/geojson-ld/geojson-context.jsonld\",\r\n    {\r\n      \"@version\": \"1.1\",\r\n      \"wx\": \"https://api.weather.gov/ontology#\",\r\n      \"geo\": \"http://www.opengis.net/ont/geosparql#\",\r\n      \"unit\": \"http://codes.wmo.int/common/unit/\",\r\n      \"@vocab\": \"https://api.weather.gov/ontology#\"\r\n    }\r\n  ],\r\n  \"type\": \"Feature\",\r\n  \"geometry\": {\r\n    \"type\": \"Polygon\",\r\n    \"coordinates\": [\r\n      [\r\n        [\r\n          -76.927978699999997,\r\n          38.867095599999999\r\n        ],\r\n        [\r\n          -76.931757199999993,\r\n          38.845150499999995\r\n        ],\r\n        [\r\n          -76.903575899999993,\r\n          38.842205499999999\r\n        ],\r\n        [\r\n          -76.899791599999986,\r\n          38.864150299999999\r\n        ],\r\n        [\r\n          -76.927978699999997,\r\n          38.867095599999999\r\n        ]\r\n      ]\r\n    ]\r\n  },\r\n  \"properties\": {\r\n    \"updated\": \"2024-01-14T15:25:41+00:00\",\r\n    \"units\": \"us\",\r\n    \"forecastGenerator\": \"BaselineForecastGenerator\",\r\n    \"generatedAt\": \"2024-01-14T17:24:38+00:00\",\r\n    \"updateTime\": \"2024-01-14T15:25:41+00:00\",\r\n    \"validTimes\": \"2024-01-14T09:00:00+00:00/P7DT16H\",\r\n    \"elevation\": {\r\n      \"unitCode\": \"wmoUnit:m\",\r\n      \"value\": 75.895200000000003\r\n    },\r\n    \"periods\": [\r\n      {\r\n        \"number\": 1,\r\n        \"name\": \"This Afternoon\",\r\n        \"startTime\": \"2024-01-14T12:00:00-05:00\",\r\n        \"endTime\": \"2024-01-14T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 43,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": \"falling\",\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 60\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -6.666666666666667\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 39\r\n        },\r\n        \"windSpeed\": \"16 to 25 mph\",\r\n        \"windDirection\": \"W\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/snow,60?size=medium\",\r\n        \"shortForecast\": \"Chance Snow Showers\",\r\n        \"detailedForecast\": \"A chance of snow showers before 3pm. Mostly sunny. High near 43, with temperatures falling to around 33 in the afternoon. West wind 16 to 25 mph, with gusts as high as 43 mph. Chance of precipitation is 60%. Little or no snow accumulation expected.\"\r\n      },\r\n      {\r\n        \"number\": 2,\r\n        \"name\": \"Tonight\",\r\n        \"startTime\": \"2024-01-14T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-15T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 23,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": null\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -13.888888888888889\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 47\r\n        },\r\n        \"windSpeed\": \"5 to 14 mph\",\r\n        \"windDirection\": \"NW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/bkn?size=medium\",\r\n        \"shortForecast\": \"Mostly Cloudy\",\r\n        \"detailedForecast\": \"Mostly cloudy, with a low around 23. Northwest wind 5 to 14 mph, with gusts as high as 25 mph.\"\r\n      },\r\n      {\r\n        \"number\": 3,\r\n        \"name\": \"M.L. King Jr. Day\",\r\n        \"startTime\": \"2024-01-15T06:00:00-05:00\",\r\n        \"endTime\": \"2024-01-15T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 33,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 20\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -8.3333333333333339\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 54\r\n        },\r\n        \"windSpeed\": \"5 mph\",\r\n        \"windDirection\": \"S\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/snow,20?size=medium\",\r\n        \"shortForecast\": \"Slight Chance Light Snow\",\r\n        \"detailedForecast\": \"A slight chance of snow after 10am. Cloudy, with a high near 33. South wind around 5 mph. Chance of precipitation is 20%.\"\r\n      },\r\n      {\r\n        \"number\": 4,\r\n        \"name\": \"Monday Night\",\r\n        \"startTime\": \"2024-01-15T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-16T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 28,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 70\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -2.2222222222222223\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 92\r\n        },\r\n        \"windSpeed\": \"6 mph\",\r\n        \"windDirection\": \"E\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/snow,40/snow,70?size=medium\",\r\n        \"shortForecast\": \"Light Snow Likely\",\r\n        \"detailedForecast\": \"Snow likely. Cloudy, with a low around 28. East wind around 6 mph. Chance of precipitation is 70%. New snow accumulation of 1 to 2 inches possible.\"\r\n      },\r\n      {\r\n        \"number\": 5,\r\n        \"name\": \"Tuesday\",\r\n        \"startTime\": \"2024-01-16T06:00:00-05:00\",\r\n        \"endTime\": \"2024-01-16T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 34,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 70\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -1.6666666666666667\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 96\r\n        },\r\n        \"windSpeed\": \"6 to 14 mph\",\r\n        \"windDirection\": \"N\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/snow,70/snow,60?size=medium\",\r\n        \"shortForecast\": \"Light Snow Likely\",\r\n        \"detailedForecast\": \"Snow likely. Cloudy, with a high near 34. North wind 6 to 14 mph, with gusts as high as 23 mph. Chance of precipitation is 70%. New snow accumulation of around one inch possible.\"\r\n      },\r\n      {\r\n        \"number\": 6,\r\n        \"name\": \"Tuesday Night\",\r\n        \"startTime\": \"2024-01-16T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-17T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 19,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 20\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -8.3333333333333339\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 68\r\n        },\r\n        \"windSpeed\": \"14 mph\",\r\n        \"windDirection\": \"NW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/snow,20/bkn?size=medium\",\r\n        \"shortForecast\": \"Slight Chance Light Snow then Mostly Cloudy\",\r\n        \"detailedForecast\": \"A slight chance of snow before 7pm. Mostly cloudy, with a low around 19. Chance of precipitation is 20%.\"\r\n      },\r\n      {\r\n        \"number\": 7,\r\n        \"name\": \"Wednesday\",\r\n        \"startTime\": \"2024-01-17T06:00:00-05:00\",\r\n        \"endTime\": \"2024-01-17T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 31,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": null\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -11.666666666666666\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 64\r\n        },\r\n        \"windSpeed\": \"9 to 13 mph\",\r\n        \"windDirection\": \"W\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/few?size=medium\",\r\n        \"shortForecast\": \"Sunny\",\r\n        \"detailedForecast\": \"Sunny, with a high near 31.\"\r\n      },\r\n      {\r\n        \"number\": 8,\r\n        \"name\": \"Wednesday Night\",\r\n        \"startTime\": \"2024-01-17T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-18T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 21,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": null\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -11.111111111111111\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 65\r\n        },\r\n        \"windSpeed\": \"9 mph\",\r\n        \"windDirection\": \"SW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/sct?size=medium\",\r\n        \"shortForecast\": \"Partly Cloudy\",\r\n        \"detailedForecast\": \"Partly cloudy, with a low around 21.\"\r\n      },\r\n      {\r\n        \"number\": 9,\r\n        \"name\": \"Thursday\",\r\n        \"startTime\": \"2024-01-18T06:00:00-05:00\",\r\n        \"endTime\": \"2024-01-18T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 38,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": null\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -6.666666666666667\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 68\r\n        },\r\n        \"windSpeed\": \"8 mph\",\r\n        \"windDirection\": \"SW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/bkn?size=medium\",\r\n        \"shortForecast\": \"Mostly Cloudy\",\r\n        \"detailedForecast\": \"Mostly cloudy, with a high near 38.\"\r\n      },\r\n      {\r\n        \"number\": 10,\r\n        \"name\": \"Thursday Night\",\r\n        \"startTime\": \"2024-01-18T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-19T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 28,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 40\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -3.3333333333333335\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 88\r\n        },\r\n        \"windSpeed\": \"3 to 7 mph\",\r\n        \"windDirection\": \"SE\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/bkn/snow,40?size=medium\",\r\n        \"shortForecast\": \"Mostly Cloudy then Chance Light Snow\",\r\n        \"detailedForecast\": \"A chance of snow after 1am. Mostly cloudy, with a low around 28. Chance of precipitation is 40%.\"\r\n      },\r\n      {\r\n        \"number\": 11,\r\n        \"name\": \"Friday\",\r\n        \"startTime\": \"2024-01-19T06:00:00-05:00\",\r\n        \"endTime\": \"2024-01-19T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 36,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 50\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -2.2222222222222223\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 88\r\n        },\r\n        \"windSpeed\": \"6 to 13 mph\",\r\n        \"windDirection\": \"N\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/snow,50?size=medium\",\r\n        \"shortForecast\": \"Chance Light Snow\",\r\n        \"detailedForecast\": \"A chance of snow. Mostly cloudy, with a high near 36. Chance of precipitation is 50%.\"\r\n      },\r\n      {\r\n        \"number\": 12,\r\n        \"name\": \"Friday Night\",\r\n        \"startTime\": \"2024-01-19T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-20T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 20,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 30\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -3.8888888888888888\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 72\r\n        },\r\n        \"windSpeed\": \"13 to 17 mph\",\r\n        \"windDirection\": \"NW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/snow,30/snow?size=medium\",\r\n        \"shortForecast\": \"Chance Light Snow\",\r\n        \"detailedForecast\": \"A chance of snow before 1am. Mostly cloudy, with a low around 20. Chance of precipitation is 30%.\"\r\n      },\r\n      {\r\n        \"number\": 13,\r\n        \"name\": \"Saturday\",\r\n        \"startTime\": \"2024-01-20T06:00:00-05:00\",\r\n        \"endTime\": \"2024-01-20T18:00:00-05:00\",\r\n        \"isDaytime\": true,\r\n        \"temperature\": 30,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": null\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -11.111111111111111\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 68\r\n        },\r\n        \"windSpeed\": \"16 to 20 mph\",\r\n        \"windDirection\": \"NW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/day/bkn?size=medium\",\r\n        \"shortForecast\": \"Partly Sunny\",\r\n        \"detailedForecast\": \"Partly sunny, with a high near 30.\"\r\n      },\r\n      {\r\n        \"number\": 14,\r\n        \"name\": \"Saturday Night\",\r\n        \"startTime\": \"2024-01-20T18:00:00-05:00\",\r\n        \"endTime\": \"2024-01-21T06:00:00-05:00\",\r\n        \"isDaytime\": false,\r\n        \"temperature\": 18,\r\n        \"temperatureUnit\": \"F\",\r\n        \"temperatureTrend\": null,\r\n        \"probabilityOfPrecipitation\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": null\r\n        },\r\n        \"dewpoint\": {\r\n          \"unitCode\": \"wmoUnit:degC\",\r\n          \"value\": -12.777777777777779\r\n        },\r\n        \"relativeHumidity\": {\r\n          \"unitCode\": \"wmoUnit:percent\",\r\n          \"value\": 64\r\n        },\r\n        \"windSpeed\": \"18 mph\",\r\n        \"windDirection\": \"NW\",\r\n        \"icon\": \"https://api.weather.gov/icons/land/night/sct?size=medium\",\r\n        \"shortForecast\": \"Partly Cloudy\",\r\n        \"detailedForecast\": \"Partly cloudy, with a low around 18.\"\r\n      }\r\n    ]\r\n  }\r\n}";

        private Mock<IApiClient> SetupMockedDataStores(LocationResult locationResult, WeatherPointResult pointResult, WeatherForecastResult forecastResult)
        {
            var apiClientMock = new Mock<IApiClient>();

            apiClientMock.Setup(client => client.getLocationDataAsync(It.IsAny<Uri>(), It.IsAny<String>()))
                         .ReturnsAsync(locationResult);
            apiClientMock.Setup(client => client.getPointDataAsync(It.IsAny<Uri>(), It.IsAny<Coordinates>(), It.IsAny<List<CustomApiHeader>>()))
                         .ReturnsAsync(pointResult);
            apiClientMock.Setup(client => client.getForecastDataAsync(It.IsAny<Uri>(), It.IsAny<List<CustomApiHeader>>()))
                         .ReturnsAsync(forecastResult);

            return apiClientMock;
        }

        [Fact]
        public async void GetWeatherForecast_ReturnsSuccess()
        {

            LocationResult locationResult = JsonConvert.DeserializeObject<LocationResult>(locationResponse)!;
            WeatherPointResult pointResult = JsonConvert.DeserializeObject<WeatherPointResult>(pointsResponse)!;
            WeatherForecastResult forecastResult = JsonConvert.DeserializeObject<WeatherForecastResult>(forecastResponse)!;

            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);

            // Act
            var result = await weatherForecast.getWeatherForecast("");
            var finalResult = (List<ForecastPeriod>)(result.Result);

            Assert.NotNull(result.Result);
            Assert.Null(result.Message);
            Assert.Equal(7, finalResult.Count);
        }
        [Fact]
        public async void GetWeatherForecast_Invalid_Address_No_Matches_ReturnsFailure()
        {
            LocationResult locationResult = new LocationResult
            {
                result = new Result
                {
                    addressMatches = new List<AddressMatch>()
                }
            };
            WeatherPointResult pointResult = new WeatherPointResult();
            WeatherForecastResult forecastResult = new WeatherForecastResult();

            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);
            var result = await weatherForecast.getWeatherForecast("");

            Assert.Equal("404", result.Message.Id);
            Assert.Equal("Unfortunately, we couldn't locate the provided address.", result.Message.Description);
        }
        [Fact]
        public async void GetWeatherForecast_Invalid_Address_Empty_Forecast_URL_ReturnsFailure()
        {
            LocationResult locationResult = JsonConvert.DeserializeObject<LocationResult>(locationResponse)!;
            WeatherPointResult pointResult = new WeatherPointResult
            {
                properties = new Point
                {
                    forecast = null,
                    gridX = 0,
                    gridY = 0,
                }
            };
            WeatherForecastResult forecastResult = new WeatherForecastResult();

            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);
            var result = await weatherForecast.getWeatherForecast("");

            Assert.Equal("1", result.Message.Id);
            Assert.Equal("Forecast url couldn't be null.", result.Message.Description);
        }
        [Fact]
        public async void GetWeatherForecast_Invalid_Address_Empty_Forecast_Periods_ReturnsFailure()
        {
            LocationResult locationResult = JsonConvert.DeserializeObject<LocationResult>(locationResponse)!;
            WeatherPointResult pointResult = JsonConvert.DeserializeObject<WeatherPointResult>(pointsResponse)!;
            WeatherForecastResult forecastResult = new WeatherForecastResult
            {
                properties = new ForecastProperties
                {
                    periods = new List<ForecastPeriod>()
                }
            };

            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);
            var result = await weatherForecast.getWeatherForecast("");

            Assert.Equal("404", result.Message.Id);
            Assert.Equal("Unfortunately, we couldn't obtain weather data for the provided address.", result.Message.Description);
        }
        [Fact]
        public async void GetWeatherForecast_getLocationDataAsync_Throw_Error()
        {
            LocationResult locationResult = new LocationResult();
            WeatherPointResult pointResult = new WeatherPointResult();
            WeatherForecastResult forecastResult = new WeatherForecastResult();


            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            apiClientMock.Setup(client => client.getLocationDataAsync(It.IsAny<Uri>(), It.IsAny<string>()))
                            .ThrowsAsync(new Exception("Simulated exception"));

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);
            var result = await weatherForecast.getWeatherForecast("");

            Assert.Equal("1", result.Message.Id);
            Assert.Equal("Simulated exception", result.Message.Description);
        }
        [Fact]
        public async void GetWeatherForecast_getPointDataAsync_Throw_Error()
        {
            LocationResult locationResult = JsonConvert.DeserializeObject<LocationResult>(locationResponse)!;
            WeatherPointResult pointResult = new WeatherPointResult();
            WeatherForecastResult forecastResult = new WeatherForecastResult();


            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            apiClientMock.Setup(client => client.getPointDataAsync(It.IsAny<Uri>(), It.IsAny<Coordinates>(), It.IsAny<List<CustomApiHeader>>()))
                            .ThrowsAsync(new Exception("Simulated exception"));

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);
            var result = await weatherForecast.getWeatherForecast("");

            Assert.Equal("1", result.Message.Id);
            Assert.Equal("Simulated exception", result.Message.Description);
        }
        [Fact]
        public async void GetWeatherForecast_getForecastDataAsync_Throw_Error()
        {
            LocationResult locationResult = JsonConvert.DeserializeObject<LocationResult>(locationResponse)!;
            WeatherPointResult pointResult = JsonConvert.DeserializeObject<WeatherPointResult>(pointsResponse)!;
            WeatherForecastResult forecastResult = new WeatherForecastResult();

            var apiClientMock = SetupMockedDataStores(locationResult, pointResult, forecastResult);

            apiClientMock.Setup(client => client.getForecastDataAsync(It.IsAny<Uri>(), It.IsAny<List<CustomApiHeader>>()))
                            .ThrowsAsync(new Exception("Simulated exception"));

            WeatherForecast weatherForecast = new WeatherForecast(apiClientMock.Object);
            var result = await weatherForecast.getWeatherForecast("");

            Assert.Equal("1", result.Message.Id);
            Assert.Equal("Simulated exception", result.Message.Description);
        }
    }
}



