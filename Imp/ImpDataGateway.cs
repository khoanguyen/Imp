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
            _expression = Expression.Constant(null, typeof(TModel));
        }

        public void Delete(TModel item)
        {
            Delete(new[] { item });
        }

        public void Delete(IEnumerable<TModel> items)
        {
            var expression = new DeleteObjectExpression(items.Cast<object>().ToArray(), typeof(TModel));
            _context.QueueExpression(expression);
        }

        public void Delete(Expression<Func<TModel, bool>> predicate)
        {
            var expression = new DeletePredicateExpression(predicate, typeof(TModel));
            _context.QueueExpression(expression);
        }

        public void Insert(TModel item, bool updateItem = true)
        {
            Insert(new[] { item }, updateItem);
        }

        public void Insert(IEnumerable<TModel> item, bool updateItem = true)
        {
            var expression = new InsertExpression(item.Cast<object>().ToArray(), updateItem, typeof(TModel));
            _context.QueueExpression(expression);
        }

        public void Update(TModel item)
        {
            Update(new[] {item});
        }

        public void Update(IEnumerable<TModel> items)
        {
            var expression = new UpdateObjectExpression(items.Cast<object>().ToArray(), typeof(TModel));
            _context.QueueExpression(expression);
        }

        public void Update(Expression<Func<TModel, bool>> predicate, Expression<Action<TModel>> update)
        {
            var expression = new UpdatePredicateExpression(predicate, update, typeof(TModel));
            _context.QueueExpression(expression);
        }
                
        public string GetQueryText()
        {
            return _provider.GetQueryText(_expression);
        }

        public IEnumerator<TModel> GetEnumerator()
        {
            var result = _provider.Execute<IEnumerable<TModel>>(this.Expression);
            return result.GetEnumerator();
            //return _provider.Execute<IEnumerable<TModel>>(this.Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _provider.Execute<IEnumerable>(this.Expression).GetEnumerator();
        }
    }
}
