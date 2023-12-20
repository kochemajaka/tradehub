using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI01.API.Dto
{
    public class PhoneDto
    {
        public Guid Id { get; set; }
        public string PhoneType { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
