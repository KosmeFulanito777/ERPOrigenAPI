using System;
using System.Drawing;
using System.IO;
using System.Web.Hosting;

namespace ERPSimulacion.Utils.Logger
{
    public class Logger
    {
        private readonly string _basePath;

        public Logger()
        {
            _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (!Directory.Exists(_basePath)){
                Directory.CreateDirectory(_basePath);
            }
        }

        public void Info(string mensaje) => WriteLog("INFO", mensaje);
        public void Error(string mensaje) => WriteLog("ERROR", mensaje);

        private void WriteLog(string tipo, string mensaje)
        {
            string nombreLog = $"log-{DateTime.Now:yyyy-MM-dd}.txt";
            string fullPath = Path.Combine(_basePath, nombreLog);

            var log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{tipo}] {mensaje}\n";
            
            File.AppendAllText(fullPath, log);
        }
    }
}