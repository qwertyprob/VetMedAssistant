using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Api.Jwt
{
    public class JwtOptions
    {
        
        public string SecretKey { get; set; }
        public int ExpiresHours { get; set; }
    }
}
