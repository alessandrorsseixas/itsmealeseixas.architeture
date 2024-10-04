using Microsoft.AspNetCore.Mvc;

namespace Its.Me.AleSeixas.Architeture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHub _sentryHub;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHub sentryHub)
        {
            _logger = logger;
            _sentryHub = sentryHub;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("additional-work");
            try
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();
                childSpan?.Finish(SpanStatus.Ok);

            }
            catch (Exception ex)
            {

                childSpan?.Finish(ex);
                throw;
            }
           
        }
    }
}