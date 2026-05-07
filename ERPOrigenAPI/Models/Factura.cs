namespace ERPOrigenAPI.Models
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public string Folio { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Total {  get; set; }
        public string Moneda { get; set; }
        public string Estatus { get; set; }
    }
}
