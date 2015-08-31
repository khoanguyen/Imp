using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp.SqlServer
{
    public class SqlServerImpProvider : BaseImpProvider
    {
        private SqlServerImpQueryProvider _queryProvider;

        public SqlServerImpProvider(ImpContextConfiguration config) : base(config) { }

        public override IImpQueryProvider GetQueryProvider()
        {
            return _queryProvider ?? (_queryProvider = new SqlServerImpQueryProvider());
        }
        
        public override System.Data.IDbConnection CreateConnection()
        {
            var connStr = this.Config.ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}
