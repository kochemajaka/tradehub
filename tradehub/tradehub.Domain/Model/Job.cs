using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradehub.Domain.Model
{
    public class Job
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}
