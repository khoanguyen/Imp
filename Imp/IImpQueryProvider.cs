using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    /// <summary>
    /// Interface for query provider implementation
    /// </summary>
    public interface IImpQueryProvider : IQueryProvider
    {
        string GetQueryText(Expression expression);
    }
}
