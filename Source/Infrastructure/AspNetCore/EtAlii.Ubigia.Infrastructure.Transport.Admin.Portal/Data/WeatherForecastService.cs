namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class WeatherForecastService
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = 21,
                Summary = Summaries[3]
            }).ToArray());
        }
    }
}
