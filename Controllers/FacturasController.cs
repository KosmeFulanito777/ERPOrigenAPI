using ERPSimulacion.Business;
using ERPSimulacion.DTOs;
using ERPSimulacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ERPSimulacion.Controllers
{
    [RoutePrefix("api/erp/facturas")]
    public class FacturasController : ApiController
    {
        private readonly FacturaService _facturaService;

        public FacturasController()
        {
            _facturaService = new FacturaService();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertarFactura(FacturaDTO dto)
        {
            var factura = new Factura
            {
                Folio = dto.Folio,
                ClientId = dto.ClientId,
                FechaFactura = dto.FechaFactura,
                FechaVencimiento = dto.FechaVencimiento,
                Total = dto.Total,
                Moneda = dto.Moneda,
                Estatus = dto.Estatus
            };

            var result = _facturaService.ProcesarFactura(factura);
            return Ok(result);
        }
    }
}