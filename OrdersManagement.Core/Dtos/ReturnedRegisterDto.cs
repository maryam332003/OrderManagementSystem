using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Core.Dtos
{
    public class ReturnedRegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Message { get; set; }
    }
}
