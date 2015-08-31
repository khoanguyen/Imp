using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp.Internal
{
    internal class ImpQueryable<TModel> : IImpQueryable<TModel>
    {
        private IImpQueryProvider _provider;
        private Expression _expression;

        internal IImpQueryProvider IrmProvider
        {
            get { return _provider; }
        }

        public ImpQueryable(IImpQueryProvider provider)
        {
            _provider = provider;
            _expression = Expression.Constant(this);
        }

        public ImpQueryable(IImpQueryProvider provider, Expression expression)
        {
            _provider = provider;
            _expression = expression;
        }

        public IEnumerator<TModel> GetEnumerator()
        {
            return _provider.Execute<IEnumerable<TModel>>(this.Expression).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _provider.Execute<IEnumerable>(this.Expression).GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(TModel); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return this._expression; }
        }

        public IQueryProvider Provider
        {
            get { return _provider; }
        }

        public string GetQueryText()
        {
            throw new NotImplementedException();
        }
    }
}
