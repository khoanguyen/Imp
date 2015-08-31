using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp.Exceptions
{
    public class ImpConfigException : ImpException
    {
        public ImpConfigException(string message) : base(message) { }
    }
}
