using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSimulacion.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Rfc { get; set; }
        public string RegimenFiscalClave { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estatus { get; set; }
    }
}