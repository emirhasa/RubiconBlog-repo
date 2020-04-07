using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;

namespace Rubicon_BlogAPI.Model
{
    public class Post
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<string> tagList { get; set; }
    }
}
