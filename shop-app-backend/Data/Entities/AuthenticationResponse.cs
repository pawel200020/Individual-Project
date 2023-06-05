using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AuthenticationResponse
    {
        public string Token { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }
    }
}
