using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class DeletePredicateExpression : ExtendedExpression
    {
        public Expression Predicate { get; private set; }

        internal DeletePredicateExpression(Expression predicate, Type type)
            : base(ExpressionTypeEx.DeleteObject, type)
        {
            Predicate = predicate;            
        }
    }
}
