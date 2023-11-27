using BabelAPI.Datos; 
using BabelAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabelAPI.Controllers 
{
    [ApiController] // Atributo que indica que esta clase es un controlador de API
    [Route("api/roles")] // Ruta base para las API en este controlador
    public class RolController : ControllerBase
    {
        private readonly DRol _rolService; // Instancia de la clase de datos para roles
        private readonly ILogger<RolController> _logger; // Logger para registrar mensajes

        // Constructor que recibe las dependencias necesarias
        public RolController(DRol rolService, ILogger<RolController> logger)
        {
            _rolService = rolService;
            _logger = logger;
        }

        // Método para obtener todos los roles (HTTP GET)
        [HttpGet]
        public async Task<ActionResult<List<MRol>>> ObtenerTodosLosRoles()
        {
            try
            {
                // Intenta obtener todos los roles a través de la instancia de DRol
                var roles = await _rolService.ObtenerTodosLosRoles();
                return roles; // Retorna la lista de roles obtenidos
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener todos los roles: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para obtener un rol por su ID (HTTP GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<MRol>> ObtenerRolPorId(int id)
        {
            try
            {
                // Intenta obtener un rol por su ID a través de la instancia de DRol
                var rol = await _rolService.ObtenerRolPorId(id);

                if (rol == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el rol no existe
                }

                return rol; // Retorna el rol obtenido
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener rol por ID: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para insertar un nuevo rol (HTTP POST)
        [HttpPost]
        public async Task<ActionResult<int>> InsertarRol([FromBody] MRol nuevoRol)
        {
            try
            {
                // Intenta insertar un nuevo rol a través de la instancia de DRol
                var nuevoRolID = await _rolService.InsertarRol(nuevoRol.rolNombre);
                return nuevoRolID; // Retorna el ID del nuevo rol insertado
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al insertar rol: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para actualizar un rol por su ID (HTTP PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] MRol rolActualizado)
        {
            try
            {
                // Intenta obtener el rol existente por su ID a través de la instancia de DRol
                var rolExistente = await _rolService.ObtenerRolPorId(id);

                if (rolExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el rol no existe
                }

                // Actualiza las propiedades necesarias
                rolExistente.rolNombre = rolActualizado.rolNombre;

                // Llama al método de actualización en DRol
                await _rolService.ActualizarRol(rolExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al actualizar rol: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para eliminar un rol por su ID (HTTP DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                // Intenta obtener el rol existente por su ID a través de la instancia de DRol
                var rolExistente = await _rolService.ObtenerRolPorId(id);

                if (rolExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el rol no existe
                }

                // Llama al método de eliminación en DRol
                await _rolService.EliminarRol(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al eliminar rol: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
