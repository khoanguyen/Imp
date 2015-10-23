using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    
    public abstract class ImpContext : IImpContext, IDisposable
    {
        private readonly Queue<Expression> _changeQueue = new Queue<Expression>();

        public ImpContextConfiguration Configuration
        {
            get;
            private set;
        }

        protected ImpContext()
        {
            Configuration = ImpContextConfiguration.GetConfig(GetDefaultConnectionStringName(this));
            Initialize();
        }

        protected ImpContext(string connectionStringName)
        {
            Configuration = new ImpContextConfiguration(connectionStringName);
            Initialize();
        }

        internal protected virtual void QueueExpression(Expression expression)
        {
            _changeQueue.Enqueue(expression);
        }

        public virtual IImpDataGateway<TModel> Gateway<TModel>()
        {
            return new ImpDataGateway<TModel>(this, Configuration.Provider.GetQueryProvider());
        }

        public virtual void SaveChanges()
        {
            using (IDbConnection connection = Configuration.Provider.CreateConnection())
            {
                try
                {                    
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var queryProvider = Configuration.Provider.GetQueryProvider();
                            while (_changeQueue.Count > 0)
                            {
                                var expression = _changeQueue.Dequeue();
                                queryProvider.Execute(expression);
                            }
                            transaction.Commit();
                        }
                        catch(Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public virtual void DiscardChanges()
        {
            _changeQueue.Clear();
        }

        protected abstract void Configure(ImpContextConfiguration config);

        private void Initialize()
        {
        }

        private static string GetDefaultConnectionStringName(object target)
        {
            return target.GetType().Name;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
