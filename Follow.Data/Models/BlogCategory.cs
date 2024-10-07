using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Data.Models
{
    public class BlogCategory : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
