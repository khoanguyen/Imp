using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp.SqlServer.Internal
{
    internal class SqlServerExpressionVisistor : ExpressionVisitor
    {
        private StringBuilder _whereBuilder;

        public SqlServerExpressionVisistor()
        {
            _whereBuilder = new StringBuilder();
        }

        public override Expression Visit(Expression node)
        {
            if (node is ExtendedExpression)
            {
                var extendedNode = node as ExtendedExpression;
                if (extendedNode.NodeTypeEx == ExpressionTypeEx.Delete)
                {                    
                    return VisitDeleteNode((DeleteExpression)extendedNode);                    
                }
            }

            return base.Visit(node);
        }

        private Expression VisitDeleteNode(DeleteExpression expression)
        {
            return expression;
        }
    }
}
