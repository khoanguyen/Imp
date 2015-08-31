using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp.SqlServer
{
    internal class SqlServerImpQueryProvider : BaseImpQueryProvider
    {

        public override IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }

        public override string GetQueryText(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }

        public override TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }

        public override object Execute(System.Linq.Expressions.Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
