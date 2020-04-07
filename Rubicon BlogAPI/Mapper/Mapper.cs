using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Rubicon_BlogAPI.Model.Requests.Insert;

namespace Rubicon_BlogAPI.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Database.Post, Model.Post>().ForMember(dest=>dest.tagList, opt => opt.MapFrom(src => src.PostTags.Select(pt=>pt.TagId)));
            CreateMap<Database.Tag, Model.Tag>();

            CreateMap<PostInsertRequest, Database.Post>();
        }
    }
}
