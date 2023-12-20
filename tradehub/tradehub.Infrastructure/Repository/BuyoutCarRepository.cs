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
    public class BuyoutCarRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public BuyoutCarRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<BuyoutCar>> GetAllAsync()
        {
            return await _context.BuyoutCars
                .Include(buyoutCar => buyoutCar.Cars)
                .Include(buyoutCar => buyoutCar.Employees)
                .OrderBy(buyoutCar => buyoutCar.BuyedOn)
                .ToListAsync();
        }

        public async Task<BuyoutCar> GetByIdAsync(Guid id)
        {
            return await _context.BuyoutCars
                .Include(buyoutCar => buyoutCar.Cars)
                .Include(buyoutCar => buyoutCar.Employees)
                .FirstOrDefaultAsync(buyoutCar => buyoutCar.Id == id);
        }

        public async Task<BuyoutCar> AddAsync(BuyoutCar buyoutCar)
        {
            _context.BuyoutCars.Add(buyoutCar);
            await _context.SaveChangesAsync();
            return buyoutCar;
        }

        public async Task UpdateAsync(BuyoutCar buyoutCar)
        {
            var existingBuyoutCar = await GetByIdAsync(buyoutCar.Id);

            if (existingBuyoutCar != null)
            {
                _context.Entry(existingBuyoutCar).CurrentValues.SetValues(buyoutCar);

                // Update related Cars
                foreach (var car in buyoutCar.Cars)
                {
                    var existingCar = existingBuyoutCar.Cars.FirstOrDefault(c => c.Id == car.Id);
                    if (existingCar == null)
                    {
                        existingBuyoutCar.Cars.Add(car);
                    }
                    else
                    {
                        _context.Entry(existingCar).CurrentValues.SetValues(car);
                    }
                }

                // Update related Employees
                foreach (var employee in buyoutCar.Employees)
                {
                    var existingEmployee = existingBuyoutCar.Employees.FirstOrDefault(e => e.Id == employee.Id);
                    if (existingEmployee == null)
                    {
                        existingBuyoutCar.Employees.Add(employee);
                    }
                    else
                    {
                        _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var buyoutCar = await _context.BuyoutCars.FindAsync(id);
            if (buyoutCar != null)
            {
                _context.BuyoutCars.Remove(buyoutCar);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}