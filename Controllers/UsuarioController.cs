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
        
        //Obtener usuario por ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<MUsuario>> Get(int id)
        {
            var usuario = await _funcion.MostrarUsuariosbyID(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        //Obtener usuarios
        [HttpGet]
        public async Task<ActionResult<List<MUsuario>>> Get()
        {
            try
            {
                var usuarios = await _funcion.MostrarUsuarios();

                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los usuarios: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        //Insertar usuario
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
        
        //Editar por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] MUsuario usuarioActualizado)
        {
            try
            {
                var usuarioExistente = await _funcion.MostrarUsuariosbyID(id);

                if (usuarioExistente == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades necesarias
                usuarioExistente.Nombre = usuarioActualizado.Nombre;
                usuarioExistente.CorreoElectronico = usuarioActualizado.CorreoElectronico;
                usuarioExistente.Contrasena = usuarioActualizado.Contrasena;
                usuarioExistente.RolID = usuarioActualizado.RolID;

                // Llamar al método de actualización en DUsuario
                await _funcion.ActualizarUsuario(usuarioExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar usuario: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
        
        //Eliminar por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var usuarioExistente = await _funcion.MostrarUsuariosbyID(id);

                if (usuarioExistente == null)
                {
                    return NotFound();
                }

                // Llamar al método de eliminación en DUsuario
                await _funcion.EliminarUsuario(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar usuario: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
