using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rubicon_BlogAPI.Model.Requests.Update
{
    public class PostUpdateRequest
    {
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(2500)]
        public string Body { get; set; }
    }
}
