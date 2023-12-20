using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using tradehub.Infrastructure;
using tradehub.Domain;
using tradehub.Domain.Model;

namespace tradehub.Infrastructure
{
    public class CarRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public CarRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars.OrderBy(p => p.Name).ToListAsync();
        }
        public async Task<Car> GetByIdAsync(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<Car> AddAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task UpdateAsync(Car car)
        {
            var existingCar = await GetByIdAsync(car.Id);

            if (existingCar != null)
            {
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

    public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
