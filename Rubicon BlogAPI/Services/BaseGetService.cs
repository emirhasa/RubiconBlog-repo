using AutoMapper;
using Rubicon_BlogAPI.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Services
{
    public class BaseGetService<TModel, TSearch, TDatabase> : IBaseGetService<TModel, TSearch> where TDatabase: class
    {
        protected readonly BlogContext _context;
        protected readonly IMapper _mapper;

        public BaseGetService(BlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual List<TModel> Get(TSearch search)
        {
            return _mapper.Map<List<TModel>>(_context.Set<TDatabase>().ToList());
        }

        public virtual TModel GetById(string id)
        {
            return _mapper.Map<TModel>(_context.Set<TDatabase>().Find(id));
        }
    }
}
