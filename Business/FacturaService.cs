using ERPSimulacion.Data;
using ERPSimulacion.DTOs;
using ERPSimulacion.Models;
using ERPSimulacion.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPSimulacion.Business
{
    public class FacturaService
    {
        private readonly FacturaRepository _repository;
        private readonly Logger _logger = new Logger();

        public FacturaService()
        {
            _repository = new FacturaRepository();
        }

        public object ProcesarFactura(Factura factura)
        {
            int intentos = 0;
            int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                try
                {
                    intentos++;
                    _logger.Info($"Intento {intentos} - Factura {factura.Folio}");

                    var result = _repository.InsertarFactura(factura);

                    _logger.Info($"Factura {factura.Folio} procesada correctamente");

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