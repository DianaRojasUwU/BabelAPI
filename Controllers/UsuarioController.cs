using BabelAPI.Datos;
using BabelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BabelAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly DUsuario _funcion;
        private readonly ILogger<UsuarioController> _logger;  // Agrega esta línea

        public UsuarioController(DUsuario funcion, ILogger<UsuarioController> logger)
        {
            _funcion = funcion;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MUsuario>> Get(int id)
        {
            var usuario = await _funcion.MostrarUsuarios(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertarUsuario([FromBody] MUsuario nuevoUsuario)
        {
            try
            {
                var nuevoUsuarioID = await _funcion.InsertarUsuario(
                    nuevoUsuario.Nombre,
                    nuevoUsuario.CorreoElectronico,
                    nuevoUsuario.Contrasena,
                    nuevoUsuario.RolID
                );

                return nuevoUsuarioID;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar usuario: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


    }
}
