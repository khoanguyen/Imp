using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public interface IImpProvider
    {
        IImpQueryProvider GetQueryProvider();
        
        IImpQueryable<TModel> GetQueryable<TModel>(Expression expression);

        //IDbTransaction CreateTransaction();

        IDbConnection CreateConnection();
    }
}
