using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit;
using tradehub.Infrastructure;
using tradehub.Domain.Model;

namespace TestProject1
{
    public class TestCarRepository
    {
        [Fact]
        public void TestAdd()
        {
            var testHelper = new TestHelper();
            var carRepository = testHelper.CarRepository;

            Car car = new Car { Id = Guid.Parse("ab00ee9b-15dc-467c-14c6-08dbf8bbcc44"), Name = "Toyota", VIN = "JAPAN112", Status = "Sale" };
            carRepository.AddAsync(car).Wait();

            Assert.Equal(Guid.Parse("ab00ee9b-15dc-467c-14c6-08dbf8bbcc44"), carRepository.GetByIdAsync(Guid.Parse("ab00ee9b-15dc-467c-14c6-08dbf8bbcc44")).Result.Id);
            Assert.True(carRepository.GetAllAsync().Result.Count == 1);
            Assert.Equal("Toyota", carRepository.GetByIdAsync(Guid.Parse("ab00ee9b-15dc-467c-14c6-08dbf8bbcc44")).Result.Name);
        }

       

        [Theory]
        [InlineData("ab00ee9b-15dc-467c-14c6-08dbf8bbcc46", "Toyota", "123456789", "Available")]
        public async Task TestUpdateCar(Guid id, string name, string vin, string status)
        {
            var testHelper = new TestHelper();
            var carRepository = testHelper.CarRepository;

            Car car = new Car { Id = id, Name = "Honda", VIN = "987654321", Status = "InUse" };
            await carRepository.AddAsync(car);

            car.Name = name;
            car.VIN = vin;
            car.Status = status;

            await carRepository.UpdateAsync(car);

            var updatedCar = await carRepository.GetByIdAsync(id);

            Assert.NotNull(updatedCar);
            Assert.Equal(name, updatedCar.Name);
            Assert.Equal(vin, updatedCar.VIN);
            Assert.Equal(status, updatedCar.Status);
        }

        [Theory]
        [InlineData("ab00ee9b-15dc-467c-14c6-08dbf8bbcc47")]
        public async Task TestDeleteCar(Guid id)
        {
            var testHelper = new TestHelper();
            var carRepository = testHelper.CarRepository;

            Car car = new Car { Id = id, Name = "Toyota", VIN = "123456789", Status = "Available" };
            await carRepository.AddAsync(car);

            await carRepository.DeleteAsync(id);

            var deletedCar = await carRepository.GetByIdAsync(id);

            Assert.Null(deletedCar);
        }

        [Fact]
        public async Task TestGetAllCars()
        {
            var testHelper = new TestHelper();
            var carRepository = testHelper.CarRepository;

            Car car1 = new Car { Id = Guid.NewGuid(), Name = "Toyota", VIN = "123456789", Status = "Available" };
            Car car2 = new Car { Id = Guid.NewGuid(), Name = "Honda", VIN = "987654321", Status = "InUse" };

            await carRepository.AddAsync(car1);
            await carRepository.AddAsync(car2);

            var allCars = await carRepository.GetAllAsync();

            Assert.NotNull(allCars);
            Assert.Equal(2, allCars.Count());
        }
    }
}
