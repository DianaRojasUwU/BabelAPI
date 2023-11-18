using BabelAPI.Datos;
using BabelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly DUsuario _dUsuario;

        public WeatherForecastController(DUsuario dUsuario)
        {
            _dUsuario = dUsuario;
        }

    }
}