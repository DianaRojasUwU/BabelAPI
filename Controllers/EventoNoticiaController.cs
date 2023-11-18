// EventoNoticiaController.cs

using BabelAPI.Datos;
using BabelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BabelAPI.Controllers
{
    [ApiController]
    [Route("api/eventosnoticias")]
    public class EventoNoticiaController : ControllerBase
    {
        private readonly DEventoNoticia _eventoNoticiaService;
        private readonly ILogger<EventoNoticiaController> _logger;

        public EventoNoticiaController(DEventoNoticia eventoNoticiaService, ILogger<EventoNoticiaController> logger)
        {
            _eventoNoticiaService = eventoNoticiaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MEventoNoticia>>> ObtenerTodosLosEventosNoticias()
        {
            try
            {
                var eventosNoticias = await _eventoNoticiaService.ObtenerTodosLosEventosNoticias();
                return eventosNoticias;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los eventos y noticias: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MEventoNoticia>> ObtenerEventoNoticiaPorId(int id)
        {
            var eventoNoticia = await _eventoNoticiaService.ObtenerEventoNoticiaPorId(id);

            if (eventoNoticia == null)
            {
                return NotFound();
            }

            return eventoNoticia;
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertarEventoNoticia([FromBody] MEventoNoticia nuevoEventoNoticia)
        {
            try
            {
                var nuevoEventoNoticiaID = await _eventoNoticiaService.InsertarEventoNoticia(nuevoEventoNoticia);
                return nuevoEventoNoticiaID;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar evento o noticia: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEventoNoticia(int id, [FromBody] MEventoNoticia eventoNoticiaActualizado)
        {
            try
            {
                var eventoNoticiaExistente = await _eventoNoticiaService.ObtenerEventoNoticiaPorId(id);

                if (eventoNoticiaExistente == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades necesarias
                eventoNoticiaExistente.Titulo = eventoNoticiaActualizado.Titulo;
                eventoNoticiaExistente.Descripcion = eventoNoticiaActualizado.Descripcion;
                eventoNoticiaExistente.FechaInicio = eventoNoticiaActualizado.FechaInicio;
                eventoNoticiaExistente.FechaFin = eventoNoticiaActualizado.FechaFin;
                eventoNoticiaExistente.Ubicacion = eventoNoticiaActualizado.Ubicacion;
                eventoNoticiaExistente.Imagen = eventoNoticiaActualizado.Imagen;
                eventoNoticiaExistente.Enlace = eventoNoticiaActualizado.Enlace;

                // Llamar al método de actualización en DEventoNoticia
                await _eventoNoticiaService.ActualizarEventoNoticia(eventoNoticiaExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar evento o noticia: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEventoNoticia(int id)
        {
            try
            {
                var eventoNoticiaExistente = await _eventoNoticiaService.ObtenerEventoNoticiaPorId(id);

                if (eventoNoticiaExistente == null)
                {
                    return NotFound();
                }

                // Llamar al método de eliminación en DEventoNoticia
                await _eventoNoticiaService.EliminarEventoNoticia(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar evento o noticia: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
