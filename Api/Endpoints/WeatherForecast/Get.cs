using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.WeatherForecast
{
    public class Get : EndpointBaseSync
        .WithoutRequest
        .WithActionResult<GetWeatherForecastResult[]>
    {
        [HttpGet("api/[namespace]/[controller]")]
        public override ActionResult<GetWeatherForecastResult[]> Handle()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var forecast = Enumerable.Range(1, 5).Select(index =>
            new GetWeatherForecastResult
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();

            return forecast;
        }
    }
}
