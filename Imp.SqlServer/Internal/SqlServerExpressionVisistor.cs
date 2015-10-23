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
        private StringBuilder _selectBuilder;
        private Expression _initialExpression;

        public bool IsSelectStatement { get; private set; }

        public SqlServerExpressionVisistor()
        {
            _whereBuilder = new StringBuilder();
            _selectBuilder = new StringBuilder();
            _initialExpression = null;
            IsSelectStatement = true;
        }

        public override Expression Visit(Expression node)
        {
            if (_initialExpression == null) _initialExpression = node;
            if (node is ExtendedExpression)
            {
                IsSelectStatement = false;
                var extendedNode = node as ExtendedExpression;
                switch(extendedNode.NodeTypeEx) {
                    case ExpressionTypeEx.DeletePredicate: return VisitDeletePredicateNode((DeletePredicateExpression)extendedNode);
                    case ExpressionTypeEx.DeleteObject: return VisitDeleteObjectNode((DeleteObjectExpression)extendedNode);                    
                }                
            }

            return base.Visit(node);
        }

        internal string GetSqlQuery()
        {
            return string.Format("{0} {1}", _selectBuilder.ToString(), _whereBuilder.ToString()).Trim();
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value == null)
            {
                _selectBuilder.Append("SELECT * FROM " + node.Type.Name);
                IsSelectStatement = true;
            }
            return base.VisitConstant(node);
        }

        private Expression VisitDeleteObjectNode(DeleteObjectExpression expression)
        {
            return expression;
        }

        private Expression VisitDeletePredicateNode(DeletePredicateExpression expression)
        {
            return expression;
        }
    }
}
