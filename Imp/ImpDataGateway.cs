using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public class ImpDataGateway<TModel> : IImpQueryable<TModel>, IImpDataGateway<TModel>
    {
        private IImpQueryProvider _provider;
        private Expression _expression;

        private ImpContext _context;
        
        public Type ElementType
        {
            get { return typeof(TModel); }
        }

        public Expression Expression
        {
            get { return _expression; }
        }

        public IQueryProvider Provider
        {
            get { return _provider; }
        }

        internal ImpDataGateway(ImpContext context, IImpQueryProvider provider)
        {
            _context = context;
            _provider = provider;
            _expression = Expression.Constant(this);
        }

        public void Delete(TModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<TModel> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TModel, bool>> predicate)
        {
            var expression = new DeleteExpression(predicate, typeof(TModel));
            _context.QueueExpression(expression);
        }

        public void Insert(TModel item, bool updateItem = true)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<TModel> item, bool updateItem = true)
        {
            var expression = new InsertExpression<TModel>(item, updateItem);
            _context.QueueExpression(expression);
        }

        public void Update(TModel item)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<TModel> item)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<TModel, bool>> predicate, Expression<Action<TModel>> update)
        {
            var expression = new UpdateExpression<TModel>(predicate, update);
            _context.QueueExpression(expression);
        }
                
        public string GetQueryText()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TModel> GetEnumerator()
        {
            return _provider.Execute<IEnumerable<TModel>>(this.Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _provider.Execute<IEnumerable>(this.Expression).GetEnumerator();
        }
    }
}
