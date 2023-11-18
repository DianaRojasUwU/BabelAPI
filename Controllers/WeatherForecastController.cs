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

        [HttpGet("{id}", Name = "GetWeatherForecast")]
        public async Task<ActionResult<MUsuario>> Get(int id)
        {
            try
            {
                var usuario = await _dUsuario.MostrarUsuarios(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                return usuario;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}