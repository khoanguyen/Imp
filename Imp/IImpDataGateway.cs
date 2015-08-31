using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public interface IImpDataGateway<TModel> : IImpQueryable<TModel>
    {
        void Delete(TModel item);
        void Delete(IEnumerable<TModel> items);
        void Delete(Expression<Func<TModel, bool>> predicate);
        void Insert(TModel item, bool updateItem = true);
        void Insert(IEnumerable<TModel> item, bool updateItem = true);
        void Update(TModel item);
        void Update(IEnumerable<TModel> item);
        void Update(Expression<Func<TModel, bool>> predicate, Expression<Action<TModel>> update);
    }
}
