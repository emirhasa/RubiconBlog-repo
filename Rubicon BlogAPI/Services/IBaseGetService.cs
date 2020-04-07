using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Services
{
    public interface IBaseGetService<TModel, TSearch>
    {
        List<TModel> Get(TSearch search);
        TModel GetById(string id);
    }
}
