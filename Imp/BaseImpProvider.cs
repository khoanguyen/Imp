using Imp.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public abstract class BaseImpProvider : IImpProvider
    {
        protected ImpContextConfiguration Config { get; private set; }

        protected BaseImpProvider(ImpContextConfiguration config)
        {
            Config = config;
        }

        public abstract IImpQueryProvider GetQueryProvider();

        public IImpQueryable<TModel> GetQueryable<TModel>(Expression expression)
        {
            return new ImpQueryable<TModel>(GetQueryProvider(), expression);
        }

        public abstract IDbConnection CreateConnection();
    }
}
