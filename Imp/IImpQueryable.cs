using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imp
{
    public interface IImpQueryable<TModel> : IQueryable<TModel>
    {
        string GetQueryText();
    }
}
