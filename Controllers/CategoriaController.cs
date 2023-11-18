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
    [Route("api/categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly DCategoria _funcion;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(DCategoria funcion, ILogger<CategoriaController> logger)
        {
            _funcion = funcion;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MCategoria>>> ObtenerTodasLasCategorias()
        {
            try
            {
                var categorias = await _funcion.ObtenerTodasLasCategorias();

                return categorias;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todas las categorías: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MCategoria>> ObtenerCategoriaPorId(int id)
        {
            var categoria = await _funcion.ObtenerCategoriaPorId(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertarCategoria([FromBody] MCategoria nuevaCategoria)
        {
            try
            {
                var nuevaCategoriaID = await _funcion.InsertarCategoria(nuevaCategoria.Nombre);

                return nuevaCategoriaID;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar categoría: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, [FromBody] MCategoria categoriaActualizada)
        {
            try
            {
                var categoriaExistente = await _funcion.ObtenerCategoriaPorId(id);

                if (categoriaExistente == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades necesarias
                categoriaExistente.Nombre = categoriaActualizada.Nombre;

                // Llamar al método de actualización en DCategoria
                await _funcion.ActualizarCategoria(categoriaExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar categoría: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            try
            {
                var categoriaExistente = await _funcion.ObtenerCategoriaPorId(id);

                if (categoriaExistente == null)
                {
                    return NotFound();
                }

                // Llamar al método de eliminación en DCategoria
                await _funcion.EliminarCategoria(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar categoría: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
