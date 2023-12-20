using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradehub.Domain.Model
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VIN { get; set; }
        public string Status { get; set; }
    }
}
