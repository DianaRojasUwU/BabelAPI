using BabelAPI.Datos; 
using BabelAPI.Models; 
using Microsoft.AspNetCore.Mvc;

namespace BabelAPI.Controllers 
{
    [ApiController] // Atributo que indica que esta clase es un controlador de API
    [Route("api/libros")] // Ruta base para las API en este controlador
    public class LibroController : ControllerBase
    {
        private readonly DLibro _funcion; // Instancia de la clase de datos para libros
        private readonly ILogger<LibroController> _logger; // Logger para registrar mensajes

        // Constructor que recibe las dependencias necesarias
        public LibroController(DLibro funcion, ILogger<LibroController> logger)
        {
            _funcion = funcion;
            _logger = logger;
        }

        // Método para obtener todos los libros (HTTP GET)
        [HttpGet]
        public async Task<ActionResult<List<MLibro>>> ObtenerTodosLosLibros()
        {
            try
            {
                // Intenta obtener todos los libros a través de la instancia de DLibro
                var libros = await _funcion.ObtenerTodosLosLibros();
                return libros; // Retorna la lista de libros obtenidos
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener todos los libros: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para obtener un libro por su ID (HTTP GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<MLibro>> ObtenerLibroPorId(int id)
        {
            try
            {
                // Intenta obtener un libro por su ID a través de la instancia de DLibro
                var libro = await _funcion.ObtenerLibroPorId(id);

                if (libro == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el libro no existe
                }

                return libro; // Retorna el libro obtenido
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener libro por ID: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para insertar un nuevo libro (HTTP POST)
        [HttpPost]
        public async Task<ActionResult<int>> InsertarLibro([FromBody] MLibro nuevoLibro)
        {
            try
            {
                // Intenta insertar un nuevo libro a través de la instancia de DLibro
                var nuevoLibroID = await _funcion.InsertarLibro(
                    nuevoLibro.Titulo,
                    nuevoLibro.Autor,
                    nuevoLibro.Descripcion,
                    nuevoLibro.Precio,
                    nuevoLibro.Stock,
                    nuevoLibro.CategoriaID
                );

                return nuevoLibroID; // Retorna el ID del nuevo libro insertado
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al insertar libro: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para actualizar un libro por su ID (HTTP PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, [FromBody] MLibro libroActualizado)
        {
            try
            {
                // Intenta obtener el libro existente por su ID a través de la instancia de DLibro
                var libroExistente = await _funcion.ObtenerLibroPorId(id);

                if (libroExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el libro no existe
                }

                // Actualiza las propiedades necesarias
                libroExistente.Titulo = libroActualizado.Titulo;
                libroExistente.Autor = libroActualizado.Autor;
                libroExistente.Descripcion = libroActualizado.Descripcion;
                libroExistente.Precio = libroActualizado.Precio;
                libroExistente.Stock = libroActualizado.Stock;
                libroExistente.CategoriaID = libroActualizado.CategoriaID;

                // Llama al método de actualización en DLibro
                await _funcion.ActualizarLibro(libroExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al actualizar libro: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para eliminar un libro por su ID (HTTP DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            try
            {
                // Intenta obtener el libro existente por su ID a través de la instancia de DLibro
                var libroExistente = await _funcion.ObtenerLibroPorId(id);

                if (libroExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el libro no existe
                }

                // Llama al método de eliminación en DLibro
                await _funcion.EliminarLibro(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al eliminar libro: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
