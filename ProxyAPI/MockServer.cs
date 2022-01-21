using ProxyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ProxyAPI
{
    public static class MockServer
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static void RunProxyApi()
        {
            var mockServer = WireMockServer.Start(5099);

            Console.WriteLine($"Mock Server started at {mockServer.Urls[0]}");

            var weatherList = SeedWeatherForecasts();

            mockServer
                .Given(Request.Create().WithPath("/weatherforecast").UsingGet())
                .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBodyAsJson(weatherList));
        }

        private static IEnumerable<WeatherForecast> SeedWeatherForecasts(int count = 10)
        {
            var rng = new Random();
            return Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
        }
    }

}
