using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubicon_BlogAPI.Database
{
    public class Tag
    {
        public Tag()
        {
            TagPosts = new HashSet<PostTag>();
        }
        public string Name { get; set; }
        
        //here it's tag posts because the class represents one tag - and this is the collection of posts tied to one tag
        //it's useful if we need to retrieve for instance all posts related to a particular tag as opposed to
        //all the tags for one post
        public ICollection<PostTag> TagPosts { get; set; }
    }
}
