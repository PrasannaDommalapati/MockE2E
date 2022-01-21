using StubWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace StubWebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var mockServer = WireMockServer.Start(5099);

            Console.WriteLine($"Mock Server started at {mockServer.Urls[0]}");

            var weatherList = new List<WeatherForecast>();

            mockServer
                .Given(Request.Create().WithPath("/weatherforecast").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK).WithBody("Hello"));

            Console.ReadKey();

            mockServer.Stop();
            mockServer.Dispose();
        }
    }
}
