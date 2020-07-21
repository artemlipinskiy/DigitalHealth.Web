using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalHealth.GlobalInterfaces
{
    public interface ILogger
    {
        void Error(string message);
        void Info(string message);
    }
}
