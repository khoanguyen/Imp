using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp.Exceptions
{
    public class ImpException : Exception
    {
        public ImpException() : base() { }
        public ImpException(string message) : base(message) { }
        public ImpException(string message, Exception inner) : base(message, inner) { }
    }
}
