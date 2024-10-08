using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow.Data.Models
{
    public class RefreshToken : BaseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool Revoked { get; set; }
        public int UserId { get; set; }
        public virtual AdminUser User { get; set; }
    }
}
