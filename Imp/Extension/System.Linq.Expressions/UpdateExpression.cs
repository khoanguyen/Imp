using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class UpdateExpression<T> : ExtendedExpression
    {
        public Expression<Func<T, bool>> Predicate { get; private set; }

        public Expression<Action<T>> Update { get; private set; }

        public UpdateExpression(Expression<Func<T, bool>> predicate, 
                                Expression<Action<T>> update)
            : base(ExpressionTypeEx.Delete, typeof(T))
        {
            Predicate = predicate;
            Update = update;
        }
    }
}
