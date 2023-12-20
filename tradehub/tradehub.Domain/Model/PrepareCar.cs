using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradehub.Domain.Model
{
    public class PrepareCar
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<DDS> PrepareRow { get; set; } = new List<DDS>();
    }
}
