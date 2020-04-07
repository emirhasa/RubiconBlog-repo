using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rubicon_BlogAPI.Model.Requests.Insert
{
    public class PostInsertRequest
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(2500)]
        public string Body { get; set; }

        public ICollection<string> tagList { get; set; }
    }
}
