using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;


namespace tradehub.API.DTO
{
    public class SaleCarDto
    {
        public Guid Id { get; set; }
        public List<CarDto> CarDtos { get; set; } = new List<CarDto>();
        public List<EmployeeDto> EmployeeDtos { get; set; } = new List<EmployeeDto>();
        public List<DDSDto> PrepareRowDtos { get; set; } = new List<DDSDto>();
    }
}