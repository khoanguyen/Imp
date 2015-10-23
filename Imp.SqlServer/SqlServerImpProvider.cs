using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp.SqlServer
{
    public class SqlServerImpProvider : BaseImpProvider
    {
        private SqlServerImpQueryProvider _queryProvider;

        internal ImpContextConfiguration ImpConfig
        {
            get { return base.Config; }
        }

        public SqlServerImpProvider(ImpContextConfiguration config)
            : base(config) { }

        public override IImpQueryProvider GetQueryProvider()
        {            
            return _queryProvider ?? (_queryProvider = new SqlServerImpQueryProvider(this));
        }
        
        public override System.Data.IDbConnection CreateConnection()
        {
            var connStr = this.Config.ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}
