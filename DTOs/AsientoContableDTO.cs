using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSimulacion.DTOs
{
    public class AsientoContableDTO
    {
        public int FacturaId { get; set; }
        public DateTime Fecha { get; set; }
        public string CuentaContable { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public string Descripcion { get; set; }
    }
}