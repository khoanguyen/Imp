using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class UpdateObjectExpression : ExtendedExpression
    {
        public ConstantExpression Targets { get; set; }

        internal UpdateObjectExpression(object[] target, Type type) :
            base(ExpressionTypeEx.UpdateObject, type)
        {
            Targets = Expression.Constant(Targets);
        }
    }
}
