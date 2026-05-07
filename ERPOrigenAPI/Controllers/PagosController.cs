

using ERPOrigenAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ERPOrigenAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public PagosController(AppDbContext context, ILogger<FacturasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _context.Pagos.ToListAsync());
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
        public async Task<IActionResult> Post(Models.Pago pago)
        {
            try
            {
                _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();
                return Ok(pago);
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