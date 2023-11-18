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
    [Route("api/libros")]
    public class LibroController : ControllerBase
    {
        private readonly DLibro _funcion;
        private readonly ILogger<LibroController> _logger;

        public LibroController(DLibro funcion, ILogger<LibroController> logger)
        {
            _funcion = funcion;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MLibro>>> ObtenerTodosLosLibros()
        {
            try
            {
                var libros = await _funcion.ObtenerTodosLosLibros();

                return libros;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los libros: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MLibro>> ObtenerLibroPorId(int id)
        {
            var libro = await _funcion.ObtenerLibroPorId(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertarLibro([FromBody] MLibro nuevoLibro)
        {
            try
            {
                var nuevoLibroID = await _funcion.InsertarLibro(
                    nuevoLibro.Titulo,
                    nuevoLibro.Autor,                    
                    nuevoLibro.Descripcion,
                    nuevoLibro.Precio,
                    nuevoLibro.Stock,
                    nuevoLibro.CategoriaID
                );

                return nuevoLibroID;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar libro: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarLibro(int id, [FromBody] MLibro libroActualizado)
        {
            try
            {
                var libroExistente = await _funcion.ObtenerLibroPorId(id);

                if (libroExistente == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades necesarias
                libroExistente.Titulo = libroActualizado.Titulo;
                libroExistente.Autor = libroActualizado.Autor;                
                libroExistente.Descripcion = libroActualizado.Descripcion;
                libroExistente.Precio = libroActualizado.Precio;
                libroExistente.Stock = libroActualizado.Stock;
                libroExistente.CategoriaID = libroActualizado.CategoriaID;

                // Llamar al método de actualización en DLibro
                await _funcion.ActualizarLibro(libroExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar libro: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarLibro(int id)
        {
            try
            {
                var libroExistente = await _funcion.ObtenerLibroPorId(id);

                if (libroExistente == null)
                {
                    return NotFound();
                }

                // Llamar al método de eliminación en DLibro
                await _funcion.EliminarLibro(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar libro: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
