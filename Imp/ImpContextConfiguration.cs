using Imp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public class ImpContextConfiguration
    {
        private static Dictionary<string, ImpContextConfiguration> _cache = new Dictionary<string, ImpContextConfiguration>();

        public static ImpContextConfiguration GetConfig(string connectionStringName)
        {
            if (_cache.ContainsKey(connectionStringName)) return _cache[connectionStringName];

            return new ImpContextConfiguration(connectionStringName);
        }

        public static void ClearCache()
        {
            _cache.Clear();
        }

        public string ConnectionString { get; private set; }
        public string ProviderName { get; private set; }
        public IImpProvider Provider { get; private set; }

        public virtual bool QueryTextSupportOnRelease { get; set; }

        internal ImpContextConfiguration(string connectionStringName)
        {            
            QueryTextSupportOnRelease = false;
            SetUpConnectionString(connectionStringName);
        }

        public override int GetHashCode()
        {
            return ConnectionString.GetHashCode();
        }

        private void SetUpConnectionString(string connectionStringName)
        {
            var connStrItem = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
            ConnectionString = connStrItem.ConnectionString;
            ProviderName = connStrItem.ProviderName;
            ValidateProvider(ProviderName);
            Provider = (IImpProvider)Activator.CreateInstance(Type.GetType(ProviderName), this);
        }

        private static void ValidateProvider(string providerName)
        {
            Func<Type, bool> predicate = t => t.FullName == providerName;

            //var providerType = AppDomain.CurrentDomain
            //                            .GetAssemblies()
            //                            .Where(asm => asm.GetTypes().Any(predicate))
            //                            .SelectMany(asm => asm.GetTypes())
            //                            .SingleOrDefault(predicate);

            var providerType = Type.GetType(providerName);

            if (providerType == null)
            {
                throw new ImpConfigException("Invlaid provider name");
            }

            if (!typeof(IImpProvider).IsAssignableFrom(providerType))
            {
                throw new ImpConfigException(providerType.FullName + " is not an ImpProvider");
            }
        }
    }
}
