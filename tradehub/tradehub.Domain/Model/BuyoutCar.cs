using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tradehub.Domain.Model
{
    public class BuyoutCar
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public string BuyedOn { get; set; }
        public int Cost { get; set; } // затраты
        public int Price { get; set; } // стоимость автомобиля
        public int Profit { get; set; } // маржа
        public string Note { get; set; }
    }
}
