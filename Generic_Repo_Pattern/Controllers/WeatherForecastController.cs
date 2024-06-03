using Generic_repo.Models;
using Generic_Repo_Pattern.Models;
using Generic_Repo_Pattern.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Generic_Repo_Pattern.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly RepositoryBase<Product> _repository;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RepositoryBase<Product> repository)
        {
            _logger = logger;
            this._repository= repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        public async Task<IEnumerable<Product>>GetAll()
        {
            var data =await _repository.GetListAsync(s => s.Name == ""  && s.ProductId.Equals(2));

            return data;
           
        }
    }
}
