using Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Model
{
    public class TestDBContext : ImpContext
    {
        public IImpDataGateway<Student> Students
        {
            get
            {
                return Gateway<Student>();
            }
        }

        protected override void Configure(ImpContextConfiguration config)
        {
            
        }
    }
}
