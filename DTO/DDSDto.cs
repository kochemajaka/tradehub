using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;


namespace tradehub.API.DTO
{
    public class DDSDto
    {
        public Guid Id { get; set; }
        public List<CarDto> CarDtos { get; set; } = new List<CarDto>();
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public string Account { get; set; }
        public string Note { get; set; }
        public bool IsProfit { get; set; }
    }
}