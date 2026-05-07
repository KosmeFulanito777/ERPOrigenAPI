using ERPSimulacion.Business;
using ERPSimulacion.DTOs;
using ERPSimulacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Http;
using System.Web.UI;

namespace ERPSimulacion.Controllers
{
    [RoutePrefix("api/erp/socionegocios")]
    public class SocioNegociosController : ApiController
    {
        private readonly ClienteService _clienteService;

        public SocioNegociosController()
        {
            _clienteService = new ClienteService();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult InsertarCliente(SociosNegocioDTO dto)
        {
            var cliente = new Cliente
            {
                ClienteId = dto.ClienteId,
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Rfc = dto.Rfc,
                RegimenFiscalClave = dto.RegimenFiscalClave,
                Estado = dto.Estado,
                Pais = dto.Pais,
                FechaRegistro = dto.FechaRegistro,
                Estatus = dto.Estatus
            };

            var result = _clienteService.ProcesarCliente(cliente);
            return Ok(result);
        }
    }
}