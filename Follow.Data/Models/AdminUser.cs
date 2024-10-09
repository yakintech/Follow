using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Data.Models
{
    public class AdminUser : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
