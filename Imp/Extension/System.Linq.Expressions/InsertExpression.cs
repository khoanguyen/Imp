using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public class InsertExpression<T> : ExtendedExpression
    {
        public IEnumerable<T> InsertedObjects { get; private set; }

        public bool UpdateItem { get; private set; }

        public InsertExpression(IEnumerable<T> inserted, bool updateItem)
            : base(ExpressionTypeEx.Insert, typeof(T))
        {
            InsertedObjects = inserted;
            UpdateItem = updateItem;
        }
    }
}
