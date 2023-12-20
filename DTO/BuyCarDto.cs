using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;


namespace tradehub.API.DTO
{
    public class BuyCarDTO
    {
        public Guid Id { get; set; }
        public List<CarDto> CarDtos { get; set; } = new List<CarDto>();
        public List<EmployeeDto> EmployeeDtos { get; set; } = new List<EmployeeDto>();
        public List<DDSDto> BuyRowDtos { get; set; } = new List<DDSDto>();
        public string BuyedOnDtos { get; set; }
    }
}