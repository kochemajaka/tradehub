using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI01.API.Dto
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public List<PhoneDto> PhoneDtos { get; set; } = new List<PhoneDto>();

    }
}
