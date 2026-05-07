using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using ERPSimulacion.DTOs;
using ERPSimulacion.Models;

namespace ERPSimulacion.Data
{
    public class FacturaRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public FacturaRepository()
        {
            _connectionFactory = new DbConnectionFactory();
        }

        public object InsertarFactura(Factura factura)
        {
            using (var conn = _connectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_InsertarFactura", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Folio", factura.Folio);
                cmd.Parameters.AddWithValue("@ClienteId", factura.ClientId);
                cmd.Parameters.AddWithValue("@FechaFactura", factura.FechaFactura);
                cmd.Parameters.AddWithValue("@FechaVencimiento", (object)factura.FechaVencimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Total", factura.Total);
                cmd.Parameters.AddWithValue("@Moneda", factura.Moneda);
                cmd.Parameters.AddWithValue("@Estatus", factura.Estatus);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new
                        {
                            FacturaId = reader["FacturaId"],
                            Estatus = reader["Estatus"]
                        };
                    }
                }
            }
            return null;
        }
    }
}