using System.Numerics;

namespace tradehub.Domain.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }

    }
}
