using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;


namespace tradehub.API.DTO
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string VIN { get; set; }
        public string Status { get; set; }
    }
}