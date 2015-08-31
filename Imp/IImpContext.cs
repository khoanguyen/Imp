using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    /// <summary>
    /// <para>
    /// Interface of Imp context.    
    /// </para>
    /// 
    /// <para>
    /// Imp is a ORM which follows Table Data Gateway pattern.
    /// More info about the pattern, read it at http://martinfowler.com/eaaCatalog/tableDataGateway.html
    /// </para>
    /// 
    /// <para>
    /// Imp context is a type of manager class of Imp. It provides
    /// <list type="bullet">
    ///     <item>
    ///         <description>Gateways for accessing to DataTable of back-end Database system.</description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         API for working with transaction. 
    ///         By default each insert/delete/update command will be executed in 1 auto transaction.
    ///         When a transaction is started insert/delete/update commands are recorded and executed when committing transaction.
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///         Entity configuration. Including : constraints, ORM-level data update events and data-mappings. 
    ///         </description>    
    ///     </item>
    /// </list>
    /// </para>
    /// </summary>
    public interface IImpContext
    {
        ImpContextConfiguration Configuration { get; }
        IImpDataGateway<TModel> Gateway<TModel>();
        void SaveChanges();
        void DiscardChanges();        
    }
}
