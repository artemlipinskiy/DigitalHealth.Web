using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalHealth.GlobalInterfaces;

namespace DigitalHealth.Services
{
    public class Logger : ILogger
    {
        private static NLog.Logger _logger = NLog.LogManager.GetLogger("Logger");

        public void Error(string message)
        {
            _logger.Error(message);
        }
        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}
