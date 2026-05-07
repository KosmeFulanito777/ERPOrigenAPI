using ERPOrigenAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPOrigenAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public ClientesController(AppDbContext context, ILogger<FacturasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            string? pais,
            DateTime? fecha,
            bool? estatus
            )
        {
            try
            {
                var query = _context.Clientes.AsQueryable();

                if (!string.IsNullOrEmpty(pais))
                {
                    query = query.Where(cliente => cliente.Pais == pais);
                }

                if (fecha.HasValue)
                {
                    query = query.Where(cliente =>
                        cliente.FechaRegistro.Date == fecha.Value.Date);
                }

                if (estatus.HasValue)
                {
                    query = query.Where(cliente => cliente.Estatus == estatus.Value);
                }

                var resultado = await query.ToListAsync();

                return Ok(resultado);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Ocurrió un error en SQL Server al procesar la solicitud.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un problema en el servidor al procesar los datos.",
                    codigo = "DB_ERROR"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en el servidor.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un error inesperado.",
                    codigo = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Models.Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return Ok(cliente);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Ocurrió un error en SQL Server al procesar la solicitud.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un problema en el servidor al procesar los datos.",
                    codigo = "DB_ERROR"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en el servidor.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un error inesperado.",
                    codigo = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Models.Cliente cliente)
        {
            try
            {
                if (id != cliente.ClienteId)
                    return BadRequest("El Id no coincide");

                var clienteExistente = await _context.Clientes.FindAsync(id);

                if (clienteExistente == null)
                    return NotFound();

                clienteExistente.Nombres = cliente.Nombres;
                clienteExistente.Apellidos = cliente.Apellidos;
                clienteExistente.Correo = cliente.Correo;
                clienteExistente.Telefono = cliente.Telefono;
                clienteExistente.Direccion = cliente.Direccion;
                clienteExistente.Rfc = cliente.Rfc;
                clienteExistente.RegimenFiscalClave = cliente.RegimenFiscalClave;
                clienteExistente.Pais = cliente.Pais;
                clienteExistente.Estatus = cliente.Estatus;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Ocurrió un error en SQL Server al procesar la solicitud.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un problema en el servidor al procesar los datos.",
                    codigo = "DB_ERROR"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en el servidor.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un error inesperado.",
                    codigo = "INTERNAL_SERVER_ERROR"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if (cliente == null)
                    return NotFound();

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Ocurrió un error en SQL Server al procesar la solicitud.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un problema en el servidor al procesar los datos.",
                    codigo = "DB_ERROR"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en el servidor.");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensaje = "Ocurrió un error inesperado.",
                    codigo = "INTERNAL_SERVER_ERROR"
                });
            }
        }
    }
}
