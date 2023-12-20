using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace tradehub.Domain.Model
{
    public class DDS
    {
        public Guid Id { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public string Account { get; set; }
        public string Note { get; set; }
        public bool IsProfit { get; set; }
    }
}
