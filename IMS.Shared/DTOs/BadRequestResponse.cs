using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Shared.DTOs
{
    public class BadRequestResponse
    {
        public string Message { get; set; }
        public string InnerException { get; set; }
    }
}
