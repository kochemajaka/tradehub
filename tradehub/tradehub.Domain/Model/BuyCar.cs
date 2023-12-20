using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradehub.Domain.Model
{
    public class BuyCar
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<DDS> BuyRow { get; set; } = new List<DDS>();
        public string BuyedOn { get; set; }
        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }
    }
}
