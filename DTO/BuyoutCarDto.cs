using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;


namespace tradehub.API.DTO
{
    public class BuyoutCarDto
    {
        public Guid Id { get; set; }
        public List<CarDto> CarDtos { get; set; } = new List<CarDto>();
        public List<EmployeeDto> EmployeeDtos { get; set; } = new List<EmployeeDto>();
        public string BuyedOn { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public int Profit { get; set; }
        public string Note { get; set; }
    }
}
