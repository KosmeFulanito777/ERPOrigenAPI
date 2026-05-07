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
    public class AsientoContableService
    {
        private readonly AsientoContableRepository _repository;
        private readonly Logger _logger = new Logger();

        public AsientoContableService()
        {
            _repository = new AsientoContableRepository();
        }

        public object ProcesarAsientoContable(AsientoContable asientoContable)
        {
            int intentos = 0;
            int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                try
                {
                    intentos++;
                    _logger.Info($"Intento {intentos} - Asiento Contable {asientoContable.FacturaId}");

                    var result = _repository.InsertarAsientoContable(asientoContable);

                    _logger.Info($"Asiento Contable {asientoContable.FacturaId} procesada correctamente");

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