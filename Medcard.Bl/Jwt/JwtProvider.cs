using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcard.Bl.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        public string GenerateToken(string id)
        {
            Console.WriteLine("sdasd");
            return string.Empty;
        }
    }
}
