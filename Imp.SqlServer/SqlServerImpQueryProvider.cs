using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Imp.SqlServer
{
    internal class SqlServerImpQueryProvider : BaseImpQueryProvider
    {

        private SqlServerImpProvider _impProvider;

        internal SqlServerImpQueryProvider(SqlServerImpProvider impProvider)
            : base()
        {
            _impProvider = impProvider;            
        }

        public override string GetQueryText(System.Linq.Expressions.Expression expression)
        {
#if DEBUG
            var visitor = new Internal.SqlServerExpressionVisistor();
            visitor.Visit(expression);
            return visitor.GetSqlQuery();
#else
            if (_config.QueryTextSupportOnRelease)
            {
                var visitor = new Internal.SqlServerExpressionVisistor();
                visitor.Visit(expression);
                return visitor.GetSqlQuery(); 
            }
            throw new NotSupportedException("")
#endif
        }

        public override TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            using (var connection = _impProvider.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return (TResult)ExecuteInternal(expression, connection);
                }
                finally
                {
                    if (connection != null) connection.Close();
                }
            }
        }

        public override object Execute(System.Linq.Expressions.Expression expression)
        {
            using (var connection = _impProvider.CreateConnection())
            {
                try
                {
                    connection.Open();
                    return ExecuteInternal(expression, connection);
                }
                finally
                {
                    if (connection != null) connection.Close();
                }
            }
        }
        
        public override object Execute(System.Linq.Expressions.Expression expression, System.Data.IDbConnection dbConnection)
        {
            return ExecuteInternal(expression, dbConnection);
        }

        public override TResult Execute<TResult>(System.Linq.Expressions.Expression expression, System.Data.IDbConnection dbConnection)
        {
            return (TResult)ExecuteInternal(expression, dbConnection);
        }

        protected object ExecuteInternal(System.Linq.Expressions.Expression expression, IDbConnection connection)
        {
            var visitor = new Internal.SqlServerExpressionVisistor();
            visitor.Visit(expression);

            var sql = visitor.GetSqlQuery();

            Debug.Assert(connection is SqlConnection, "Wrong type of connection");
            
            var sqlConn = (SqlConnection)connection;

            var cmd = sqlConn.CreateCommand();
            cmd.CommandText = sql;

            if (visitor.IsSelectStatement)
            {
                var result = CastToIEnumerableOfType(ExecuteSelectCommand(cmd, expression.Type), expression.Type);

                return result;
            }

            return null;
        }

        private IEnumerable<object> ExecuteSelectCommand(SqlCommand command, Type objType)
        {
            List<object> result = new List<object>();
            var reader = command.ExecuteReader();

            var propertyInfos = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {                
                var instance = Activator.CreateInstance(objType);
                foreach (var p in propertyInfos)
                {
                    SetValueOnProperty(p, instance, reader[p.Name]);
                }

                result.Add(instance);
            }

            return result;
        }

        private static object CastToIEnumerableOfType(IEnumerable<object> objSet, Type objType)
        {
            var castMethod = typeof(System.Linq.Enumerable).GetMethod("Cast");
            var genericCast = castMethod.MakeGenericMethod(objType);
            return genericCast.Invoke(null, new object[] { objSet });
        }

        private static void SetValueOnProperty(PropertyInfo prop, object target, object value)
        {
            var backField = prop.DeclaringType.GetField(GetBackingFieldName(prop.Name));
            if (backField == null)
            {
                if (prop.CanWrite)
                {
                    prop.SetValue(target, value);
                }
            }
            else
            {
                backField.SetValue(target, value);
            }
        }

        private static string GetBackingFieldName(string propertyName)
        {
            return string.Format("<{0}>k__BackingField", propertyName);
        }
    }
}
