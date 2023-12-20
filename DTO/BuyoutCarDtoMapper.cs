
using tradehub.Domain.Model;

namespace tradehub.API.DTO
{
    public class BuyoutCarMapper
    {
        public static BuyoutCarDto MapToDto(BuyoutCar buyoutCar)
        {
            return new BuyoutCarDto
            {
                Id = buyoutCar.Id,
                CarDtos = buyoutCar.Cars.Select(CarMapper.MapToDto).ToList(),
                EmployeeDtos = buyoutCar.Employees.Select(EmployeeMapper.MapToDto).ToList(),
                BuyedOn = buyoutCar.BuyedOn,
                Cost = buyoutCar.Cost,
                Price = buyoutCar.Price,
                Profit = buyoutCar.Profit,
                Note = buyoutCar.Note
            };
        }

        public static BuyoutCar MapToEntity(BuyoutCarDto buyoutCarDto)
        {
            return new BuyoutCar
            {
                Id = buyoutCarDto.Id,
                Cars = buyoutCarDto.CarDtos.Select(CarMapper.MapToEntity).ToList(),
                Employees = buyoutCarDto.EmployeeDtos.Select(EmployeeMapper.MapToEntity).ToList(),
                BuyedOn = buyoutCarDto.BuyedOn,
                Cost = buyoutCarDto.Cost,
                Price = buyoutCarDto.Price,
                Profit = buyoutCarDto.Profit,
                Note = buyoutCarDto.Note
            };
        }
    }
}