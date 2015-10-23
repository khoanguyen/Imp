using Imp.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public abstract class BaseImpQueryProvider : IImpQueryProvider
    {
        protected BaseImpQueryProvider() { }

        public virtual IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            return new ImpQueryable<TElement>(this, expression);
        }

        public virtual IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {
            var type = expression.Type;
            var resultType = typeof(ImpQueryable<>).MakeGenericType(type);
            return ((IQueryable)Activator.CreateInstance(resultType, new object[] { this, expression }));
        }

        public abstract string GetQueryText(System.Linq.Expressions.Expression expression);


        public abstract TResult Execute<TResult>(System.Linq.Expressions.Expression expression);

        public abstract object Execute(System.Linq.Expressions.Expression expression);
        
        public abstract object Execute(System.Linq.Expressions.Expression expression, System.Data.IDbConnection dbConnection);

        public abstract TResult Execute<TResult>(System.Linq.Expressions.Expression expression, System.Data.IDbConnection dbConnection);
    }
}
