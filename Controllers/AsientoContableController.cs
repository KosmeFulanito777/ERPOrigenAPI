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
    [RoutePrefix("api/erp/asientoscontables")]
    public class AsientoContableController : ApiController
    {
        private readonly AsientoContableService _asientoContableService;

        public AsientoContableController()
        {
            _asientoContableService = new AsientoContableService();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertarAsientoContable(AsientoContableDTO dto)
        {
            var asientoContable = new AsientoContable
            {
                FacturaId = dto.FacturaId,
                Fecha = dto.Fecha,
                CuentaContable = dto.CuentaContable,
                Debe = dto.Debe,
                Haber = dto.Haber,
                Descripcion = dto.Descripcion
            };

            var result = _asientoContableService.ProcesarAsientoContable(asientoContable);
            return Ok(result);
        }
    }
}