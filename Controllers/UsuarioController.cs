using BabelAPI.Datos; 
using BabelAPI.Models;
using Microsoft.AspNetCore.Mvc; 

namespace BabelAPI.Controllers 
{
    [ApiController] // Atributo que indica que esta clase es un controlador de API
    [Route("api/usuarios")] // Ruta base para las API en este controlador
    public class UsuarioController : ControllerBase
    {
        private readonly DUsuario _funcion; // Instancia de la clase de datos para usuarios
        private readonly ILogger<UsuarioController> _logger; // Logger para registrar mensajes

        // Constructor que recibe las dependencias necesarias
        public UsuarioController(DUsuario funcion, ILogger<UsuarioController> logger)
        {
            _funcion = funcion;
            _logger = logger;
        }

        // Método para obtener un usuario por su ID (HTTP GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<MUsuario>> Get(int id)
        {
            var usuario = await _funcion.MostrarUsuariosbyID(id);

            if (usuario == null)
            {
                return NotFound(); // Retorna un código de estado 404 si el usuario no existe
            }

            return usuario; // Retorna el usuario obtenido
        }

        // Método para obtener todos los usuarios (HTTP GET)
        [HttpGet]
        public async Task<ActionResult<List<MUsuario>>> Get()
        {
            try
            {
                // Intenta obtener todos los usuarios a través de la instancia de DUsuario
                var usuarios = await _funcion.MostrarUsuarios();

                return usuarios; // Retorna la lista de usuarios obtenidos
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener todos los usuarios: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para insertar un nuevo usuario (HTTP POST)
        [HttpPost]
        public async Task<ActionResult<int>> InsertarUsuario([FromBody] MUsuario nuevoUsuario)
        {
            try
            {
                // Intenta insertar un nuevo usuario a través de la instancia de DUsuario
                var nuevoUsuarioID = await _funcion.InsertarUsuario(
                    nuevoUsuario.Nombre,
                    nuevoUsuario.CorreoElectronico,
                    nuevoUsuario.Contrasena,
                    nuevoUsuario.RolID
                );

                return nuevoUsuarioID; // Retorna el ID del nuevo usuario insertado
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al insertar usuario: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para actualizar un usuario por su ID (HTTP PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] MUsuario usuarioActualizado)
        {
            try
            {
                // Intenta obtener el usuario existente por su ID a través de la instancia de DUsuario
                var usuarioExistente = await _funcion.MostrarUsuariosbyID(id);

                if (usuarioExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el usuario no existe
                }

                // Actualiza las propiedades necesarias
                usuarioExistente.Nombre = usuarioActualizado.Nombre;
                usuarioExistente.CorreoElectronico = usuarioActualizado.CorreoElectronico;
                usuarioExistente.Contrasena = usuarioActualizado.Contrasena;
                usuarioExistente.RolID = usuarioActualizado.RolID;

                // Llama al método de actualización en DUsuario
                await _funcion.ActualizarUsuario(usuarioExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al actualizar usuario: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para eliminar un usuario por su ID (HTTP DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                // Intenta obtener el usuario existente por su ID a través de la instancia de DUsuario
                var usuarioExistente = await _funcion.MostrarUsuariosbyID(id);

                if (usuarioExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el usuario no existe
                }

                // Llama al método de eliminación en DUsuario
                await _funcion.EliminarUsuario(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al eliminar usuario: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
