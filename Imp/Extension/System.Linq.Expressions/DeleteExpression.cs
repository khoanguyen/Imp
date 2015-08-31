using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class DeleteExpression : ExtendedExpression
    {
        public Expression Predicate { get; private set; }

        public DeleteExpression(Expression predicate, Type type)
            : base(ExpressionTypeEx.Delete, type)
        {
            Predicate = predicate;
        }
    }
}
