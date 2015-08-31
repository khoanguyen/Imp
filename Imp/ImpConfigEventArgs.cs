using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public class ImpConfigEventArgs : EventArgs
    {
        public ImpContextConfiguration Configuration { get; set; }

        public ImpConfigEventArgs(ImpContextConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
