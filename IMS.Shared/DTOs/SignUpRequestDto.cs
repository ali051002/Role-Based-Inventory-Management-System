using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Shared.DTOs
{
    public class SignUpRequestDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
