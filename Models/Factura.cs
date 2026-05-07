using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSimulacion.Models
{
    public class Factura
    {
        public string Folio { get; set; }
        public int ClientId { get; set; }
        public DateTime FechaFactura { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public decimal Total { get; set; }

        public string Moneda { get; set; }
        public string Estatus { get; set; }
    }
}