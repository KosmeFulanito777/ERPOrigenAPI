using ERPSimulacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ERPSimulacion.Data
{
    public class ClienteRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ClienteRepository()
        {
            _connectionFactory = new DbConnectionFactory();
        }

        public object InsertarCliente(Cliente cliente)
        {
            using (var conn = _connectionFactory.CreateConnection())
            using (var cmd = new SqlCommand("sp_InsertarCliente", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Rfc", cliente.Rfc);
                cmd.Parameters.AddWithValue("@RegimenFiscalClave", cliente.RegimenFiscalClave);
                cmd.Parameters.AddWithValue("@Estado", cliente.Estado);
                cmd.Parameters.AddWithValue("@Pais", cliente.Pais);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new
                        {
                            ClienteId = reader["ClienteId"],
                            Estatus = reader["Estatus"]
                        };
                    }
                }
            }
            return null;
        }
    }
}