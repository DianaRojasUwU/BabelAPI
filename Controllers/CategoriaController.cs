using BabelAPI.Datos; 
using BabelAPI.Models;
using Microsoft.AspNetCore.Mvc; 

namespace BabelAPI.Controllers 
{
    [ApiController] // Atributo que indica que esta clase es un controlador de API
    [Route("api/categorias")] // Ruta base para las API en este controlador
    public class CategoriaController : ControllerBase
    {
        private readonly DCategoria _funcion; // Instancia de la clase de datos para categorías
        private readonly ILogger<CategoriaController> _logger; // Logger para registrar mensajes

        // Constructor que recibe las dependencias necesarias
        public CategoriaController(DCategoria funcion, ILogger<CategoriaController> logger)
        {
            _funcion = funcion;
            _logger = logger;
        }

        // Método para obtener todas las categorías (HTTP GET)
        [HttpGet]
        public async Task<ActionResult<List<MCategoria>>> ObtenerTodasLasCategorias()
        {
            try
            {
                // Intenta obtener todas las categorías a través de la instancia de DCategoria
                var categorias = await _funcion.ObtenerTodasLasCategorias();
                return categorias; // Retorna la lista de categorías obtenidas
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener todas las categorías: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para obtener una categoría por su ID (HTTP GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<MCategoria>> ObtenerCategoriaPorId(int id)
        {
            try
            {
                // Intenta obtener una categoría por su ID a través de la instancia de DCategoria
                var categoria = await _funcion.ObtenerCategoriaPorId(id);

                if (categoria == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si la categoría no existe
                }

                return categoria; // Retorna la categoría obtenida
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener categoría por ID: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para insertar una nueva categoría (HTTP POST)
        [HttpPost]
        public async Task<ActionResult<int>> InsertarCategoria([FromBody] MCategoria nuevaCategoria)
        {
            try
            {
                // Intenta insertar una nueva categoría a través de la instancia de DCategoria
                var nuevaCategoriaID = await _funcion.InsertarCategoria(nuevaCategoria.Nombre);
                return nuevaCategoriaID; // Retorna el ID de la nueva categoría insertada
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al insertar categoría: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para actualizar una categoría por su ID (HTTP PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] MCategoria categoriaActualizada)
        {
            try
            {
                // Intenta obtener la categoría existente por su ID a través de la instancia de DCategoria
                var categoriaExistente = await _funcion.ObtenerCategoriaPorId(id);

                if (categoriaExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si la categoría no existe
                }

                // Actualiza las propiedades necesarias
                categoriaExistente.Nombre = categoriaActualizada.Nombre;

                // Llama al método de actualización en DCategoria
                await _funcion.ActualizarCategoria(categoriaExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al actualizar categoría: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para eliminar una categoría por su ID (HTTP DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            try
            {
                // Intenta obtener la categoría existente por su ID a través de la instancia de DCategoria
                var categoriaExistente = await _funcion.ObtenerCategoriaPorId(id);

                if (categoriaExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si la categoría no existe
                }

                // Llama al método de eliminación en DCategoria
                await _funcion.EliminarCategoria(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al eliminar categoría: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
