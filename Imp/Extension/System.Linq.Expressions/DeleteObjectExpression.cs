using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class DeleteObjectExpression : ExtendedExpression
    {
        public ConstantExpression Target { get; private set; }

        internal DeleteObjectExpression(object[] target, Type type)
            : base(ExpressionTypeEx.DeleteObject, type)
        {
            Target = Expression.Constant(target);
        }
    }
}
