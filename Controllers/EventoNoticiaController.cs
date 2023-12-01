using BabelAPI.Datos; 
using BabelAPI.Models; 
using Microsoft.AspNetCore.Mvc;

namespace BabelAPI.Controllers 
{
    [ApiController] // Atributo que indica que esta clase es un controlador de API
    [Route("api/eventosnoticias")] // Ruta base para las API en este controlador
    public class EventoNoticiaController : ControllerBase
    {
        private readonly DEventoNoticia _eventoNoticiaService; // Instancia de la clase de datos para eventos y noticias
        private readonly ILogger<EventoNoticiaController> _logger; // Logger para registrar mensajes

        // Constructor que recibe las dependencias necesarias
        public EventoNoticiaController(DEventoNoticia eventoNoticiaService, ILogger<EventoNoticiaController> logger)
        {
            _eventoNoticiaService = eventoNoticiaService;
            _logger = logger;
        }

        // Método para obtener todas los eventos y noticias (HTTP GET)
        [HttpGet]
        public async Task<ActionResult<List<MEventoNoticia>>> ObtenerTodosLosEventosNoticias()
        {
            try
                //debuger
            {
                // Intenta obtener todos los eventos y noticias a través de la instancia de DEventoNoticia
                var eventosNoticias = await _eventoNoticiaService.ObtenerTodosLosEventosNoticias();
                return eventosNoticias; // Retorna la lista de eventos y noticias obtenidos
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener todos los eventos y noticias: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para obtener un evento o noticia por su ID (HTTP GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<MEventoNoticia>> ObtenerEventoNoticiaPorId(int id)
        {
            try
            {
                // Intenta obtener un evento o noticia por su ID a través de la instancia de DEventoNoticia
                var eventoNoticia = await _eventoNoticiaService.ObtenerEventoNoticiaPorId(id);

                if (eventoNoticia == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el evento o noticia no existe
                }

                return eventoNoticia; // Retorna el evento o noticia obtenido
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al obtener evento o noticia por ID: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para insertar un nuevo evento o noticia (HTTP POST)
        [HttpPost]
        public async Task<ActionResult<int>> InsertarEventoNoticia([FromBody] MEventoNoticia nuevoEventoNoticia)
        {
            try
            {
                // Intenta insertar un nuevo evento o noticia a través de la instancia de DEventoNoticia
                var nuevoEventoNoticiaID = await _eventoNoticiaService.InsertarEventoNoticia(nuevoEventoNoticia);
                return nuevoEventoNoticiaID; // Retorna el ID del nuevo evento o noticia insertado
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al insertar evento o noticia: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para actualizar un evento o noticia por su ID (HTTP PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEventoNoticia(int id, [FromBody] MEventoNoticia eventoNoticiaActualizado)
        {
            try
            {
                // Intenta obtener el evento o noticia existente por su ID a través de la instancia de DEventoNoticia
                var eventoNoticiaExistente = await _eventoNoticiaService.ObtenerEventoNoticiaPorId(id);

                if (eventoNoticiaExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el evento o noticia no existe
                }

                // Actualiza las propiedades necesarias
                eventoNoticiaExistente.Titulo = eventoNoticiaActualizado.Titulo;
                eventoNoticiaExistente.Descripcion = eventoNoticiaActualizado.Descripcion;
                eventoNoticiaExistente.FechaInicio = eventoNoticiaActualizado.FechaInicio;
                eventoNoticiaExistente.FechaFin = eventoNoticiaActualizado.FechaFin;
                eventoNoticiaExistente.Ubicacion = eventoNoticiaActualizado.Ubicacion;
                eventoNoticiaExistente.Imagen = eventoNoticiaActualizado.Imagen;
                eventoNoticiaExistente.Enlace = eventoNoticiaActualizado.Enlace;

                // Llama al método de actualización en DEventoNoticia
                await _eventoNoticiaService.ActualizarEventoNoticia(eventoNoticiaExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al actualizar evento o noticia: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Método para eliminar un evento o noticia por su ID (HTTP DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEventoNoticia(int id)
        {
            try
            {
                // Intenta obtener el evento o noticia existente por su ID a través de la instancia de DEventoNoticia
                var eventoNoticiaExistente = await _eventoNoticiaService.ObtenerEventoNoticiaPorId(id);

                if (eventoNoticiaExistente == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si el evento o noticia no existe
                }

                // Llama al método de eliminación en DEventoNoticia
                await _eventoNoticiaService.EliminarEventoNoticia(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                // Log del error y retorno de un código de estado 500 en caso de error
                _logger.LogError($"Error al eliminar evento o noticia: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
