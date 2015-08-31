using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
    public abstract class ExtendedExpression : Expression
    {
        private Type _type;

        public override Type Type
        {
            get
            {
                return _type;
            }
        }

        public override ExpressionType NodeType
        {
            get
            {
                return ExpressionType.Extension;
            }
        }

        public ExpressionTypeEx NodeTypeEx { get; private set; }

        protected ExtendedExpression(ExpressionTypeEx nodeTypeEx, Type type)
            : base()
        {
            _type = type;
            NodeTypeEx = nodeTypeEx;
        }
    }
}
