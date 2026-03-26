using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wasla.Service.DTOs.Auth
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
    }
}
