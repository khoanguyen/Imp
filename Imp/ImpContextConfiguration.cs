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
            Provider = (IImpProvider)Activator.CreateInstance(Type.GetType(ProviderName));
        }

        private static void ValidateProvider(string providerName)
        {
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
