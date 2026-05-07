using ERPSimulacion.DTOs;
using ERPSimulacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPSimulacion.Data
{
    public class AsientoContableRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public AsientoContableRepository()
        {
            _connectionFactory = new DbConnectionFactory();
        }

        public object InsertarAsientoContable(AsientoContable asientoContable)
        {
            using (var conn = _connectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_InsertarAsientoContable", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacturaId", asientoContable.FacturaId);
                cmd.Parameters.AddWithValue("@CuentaContable", asientoContable.CuentaContable);
                cmd.Parameters.AddWithValue("@Debe", asientoContable.Debe);
                cmd.Parameters.AddWithValue("@Haber", asientoContable.Haber);
                cmd.Parameters.AddWithValue("@Descripcion", asientoContable.Descripcion);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new
                        {
                            AsientoId = reader["AsientoId"],
                            Estatus = reader["Estatus"]
                        };
                    }
                }
                return null;
            }
        }
    }
}