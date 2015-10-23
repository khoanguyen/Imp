using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class UpdatePredicateExpression : ExtendedExpression
    {
        public Expression Predicate { get; private set; }

        public Expression Update { get; private set; }

        internal UpdatePredicateExpression(Expression predicate, 
                                Expression update, 
                                Type type)
            : base(ExpressionTypeEx.DeleteObject, type)
        {
            Predicate = predicate;
            Update = update;
        }
    }
}
