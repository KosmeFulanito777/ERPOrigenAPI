using ERPSimulacion.Data;
using ERPSimulacion.Models;
using ERPSimulacion.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPSimulacion.Business
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;
        private readonly Logger _logger = new Logger();

        public ClienteService()
        {
            _repository = new ClienteRepository();
        }

        public object ProcesarCliente(Cliente cliente)
        {
            int intentos = 0;
            int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                try
                {
                    intentos++;
                    _logger.Info($"Intento {intentos} - Cliente {cliente.Rfc}");

                    var result = _repository.InsertarCliente( cliente );

                    _logger.Info($"Cliente {cliente.Rfc} procesado correctamente");

                    return new
                    {
                        Estatus = "Procesado",
                        Intentos = intentos,
                        Resultado = result
                    };
                }
                catch (SqlException ex)
                {
                    _logger.Info($"Error en intento {intentos} {ex.Message}");
                    if (intentos >= maxIntentos)
                    {
                        return new
                        {
                            Estatus = "Error",
                            Mensaje = ex.Message,
                        };
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    _logger.Info($"Error en intento {intentos} {ex.Message}");
                    if (intentos >= maxIntentos)
                    {
                        return new
                        {
                            Estatus = "Error",
                            Mensaje = ex.Message,
                        };
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            return null;
        }
    }
}