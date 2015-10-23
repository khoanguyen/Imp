using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class InsertExpression : ExtendedExpression
    {
        public ConstantExpression InsertedObjects { get; private set; }

        public bool UpdateItem { get; private set; }

        internal InsertExpression(object[] inserted, bool updateItem, Type type)
            : base(ExpressionTypeEx.Insert, type)
        {
            InsertedObjects = Expression.Constant(inserted);
            UpdateItem = updateItem;
        }
    }
}
