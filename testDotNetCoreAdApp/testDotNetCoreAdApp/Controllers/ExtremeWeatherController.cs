using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using testDotNetCoreAdApp.Policies;

namespace testDotNetCoreAdApp.Controllers
{
    [AuthorizeTestAdmin]
    [Route("api/[controller]")]
    [ApiController]
    public class ExtremeWeatherController : ControllerBase
    {
        private static string[] Summaries = new[]
        {
            "Fucking Freezing", "Right Bracing", "Chilly As", "Cool Beans", "Particularly Mild", "Damn Warm", "Bastard Balmy", "Hot Hot Hot", "Sweaty Betty", "Mordor"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}