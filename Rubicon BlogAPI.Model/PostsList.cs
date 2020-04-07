using System;
using System.Collections.Generic;
using System.Text;

namespace Rubicon_BlogAPI.Model
{
    public class PostsList
    {
        public ICollection<Post> blogPosts { get; set; }
        public int postsCount { get; set; }
    }
}
