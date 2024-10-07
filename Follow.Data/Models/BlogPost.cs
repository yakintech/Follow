using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Data.Models
{
    public class BlogPost : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogCategoryId { get; set; }

        [ForeignKey("BlogCategoryId")]
        public BlogCategory BlogCategory { get; set; }
    }
}
