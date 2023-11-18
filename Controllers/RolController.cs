// RolController.cs

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
    [Route("api/roles")]
    public class RolController : ControllerBase
    {
        private readonly DRol _rolService;
        private readonly ILogger<RolController> _logger;

        public RolController(DRol rolService, ILogger<RolController> logger)
        {
            _rolService = rolService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MRol>>> ObtenerTodosLosRoles()
        {
            try
            {
                var roles = await _rolService.ObtenerTodosLosRoles();
                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los roles: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MRol>> ObtenerRolPorId(int id)
        {
            var rol = await _rolService.ObtenerRolPorId(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        [HttpPost]
        public async Task<ActionResult<int>> InsertarRol([FromBody] MRol nuevoRol)
        {
            try
            {
                var nuevoRolID = await _rolService.InsertarRol(nuevoRol.rolNombre);
                return nuevoRolID;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al insertar rol: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] MRol rolActualizado)
        {
            try
            {
                var rolExistente = await _rolService.ObtenerRolPorId(id);

                if (rolExistente == null)
                {
                    return NotFound();
                }

                // Actualizar las propiedades necesarias
                rolExistente.rolNombre = rolActualizado.rolNombre;

                // Llamar al método de actualización en DRol
                await _rolService.ActualizarRol(rolExistente);

                return NoContent(); // Indica que la actualización fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar rol: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                var rolExistente = await _rolService.ObtenerRolPorId(id);

                if (rolExistente == null)
                {
                    return NotFound();
                }

                // Llamar al método de eliminación en DRol
                await _rolService.EliminarRol(id);

                return NoContent(); // Indica que la eliminación fue exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar rol: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
