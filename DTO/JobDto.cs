using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;


namespace tradehub.API.DTO
{
    public class JobDto
    {
        public Guid Id { get; set; }
        public List<CarDto> CarDtos { get; set; } = new List<CarDto>();
        public List<EmployeeDto> EmployeeDtos { get; set; } = new List<EmployeeDto>();
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
    }
}