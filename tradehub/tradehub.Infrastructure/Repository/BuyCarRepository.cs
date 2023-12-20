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
    public class BuyCarRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public BuyCarRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<BuyCar>> GetAllAsync()
        {
            return await _context.BuyCars
                .Include(buyCar => buyCar.Cars)
                .Include(buyCar => buyCar.Employees)
                .Include(buyCar => buyCar.BuyRow)
                .OrderBy(buyCar => buyCar.BuyedOn)
                .ToListAsync();
        }

        public async Task<BuyCar> GetByIdAsync(Guid id)
        {
            return await _context.BuyCars
                .Include(buyCar => buyCar.Cars)
                .Include(buyCar => buyCar.Employees)
                .Include(buyCar => buyCar.BuyRow)
                .FirstOrDefaultAsync(buyCar => buyCar.Id == id);
        }

        public async Task<BuyCar> AddAsync(BuyCar buyCar)
        {
            _context.BuyCars.Add(buyCar);
            await _context.SaveChangesAsync();
            return buyCar;
        }

        public async Task UpdateAsync(BuyCar buyCar)
        {
            var existingBuyCar = await GetByIdAsync(buyCar.Id);

            if (existingBuyCar != null)
            {
                _context.Entry(existingBuyCar).CurrentValues.SetValues(buyCar);

                // Update related Cars
                foreach (var car in buyCar.Cars)
                {
                    var existingCar = existingBuyCar.Cars.FirstOrDefault(c => c.Id == car.Id);
                    if (existingCar == null)
                    {
                        existingBuyCar.Cars.Add(car);
                    }
                    else
                    {
                        _context.Entry(existingCar).CurrentValues.SetValues(car);
                    }
                }

                // Update related Employees
                foreach (var employee in buyCar.Employees)
                {
                    var existingEmployee = existingBuyCar.Employees.FirstOrDefault(e => e.Id == employee.Id);
                    if (existingEmployee == null)
                    {
                        existingBuyCar.Employees.Add(employee);
                    }
                    else
                    {
                        _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    }
                }

                // Update related BuyRow
                foreach (var buyRow in buyCar.BuyRow)
                {
                    var existingBuyRow = existingBuyCar.BuyRow.FirstOrDefault(b => b.Id == buyRow.Id);
                    if (existingBuyRow == null)
                    {
                        existingBuyCar.BuyRow.Add(buyRow);
                    }
                    else
                    {
                        _context.Entry(existingBuyRow).CurrentValues.SetValues(buyRow);
                    }
                }

                // Handle deleted related Cars
                foreach (var existingCar in existingBuyCar.Cars.ToList())
                {
                    if (!buyCar.Cars.Any(c => c.Id == existingCar.Id))
                    {
                        existingBuyCar.Cars.Remove(existingCar);
                    }
                }

                // Handle deleted related Employees
                foreach (var existingEmployee in existingBuyCar.Employees.ToList())
                {
                    if (!buyCar.Employees.Any(e => e.Id == existingEmployee.Id))
                    {
                        existingBuyCar.Employees.Remove(existingEmployee);
                    }
                }

                // Handle deleted related BuyRow
                foreach (var existingBuyRow in existingBuyCar.BuyRow.ToList())
                {
                    if (!buyCar.BuyRow.Any(b => b.Id == existingBuyRow.Id))
                    {
                        existingBuyCar.BuyRow.Remove(existingBuyRow);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var buyCar = await _context.BuyCars.FindAsync(id);
            if (buyCar != null)
            {
                _context.BuyCars.Remove(buyCar);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
