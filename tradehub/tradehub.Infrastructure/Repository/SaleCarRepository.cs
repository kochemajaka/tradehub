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
    public class SaleCarRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public SaleCarRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<SaleCar>> GetAllAsync()
        {
            return await _context.SaleCars
                .Include(saleCar => saleCar.Cars)
                .Include(saleCar => saleCar.Employees)
                .Include(saleCar => saleCar.BuyRow)
                .ToListAsync();
        }

        public async Task<SaleCar> GetByIdAsync(Guid id)
        {
            return await _context.SaleCars
                .Include(saleCar => saleCar.Cars)
                .Include(saleCar => saleCar.Employees)
                .Include(saleCar => saleCar.BuyRow)
                .FirstOrDefaultAsync(saleCar => saleCar.Id == id);
        }

        public async Task<SaleCar> AddAsync(SaleCar saleCar)
        {
            _context.SaleCars.Add(saleCar);
            await _context.SaveChangesAsync();
            return saleCar;
        }

        public async Task UpdateAsync(SaleCar saleCar)
        {
            var existingSaleCar = await GetByIdAsync(saleCar.Id);

            if (existingSaleCar != null)
            {
                _context.Entry(existingSaleCar).CurrentValues.SetValues(saleCar);

                // Update related Cars
                foreach (var car in saleCar.Cars)
                {
                    var existingCar = existingSaleCar.Cars.FirstOrDefault(c => c.Id == car.Id);
                    if (existingCar == null)
                    {
                        existingSaleCar.Cars.Add(car);
                    }
                    else
                    {
                        _context.Entry(existingCar).CurrentValues.SetValues(car);
                    }
                }

                // Update related Employees
                foreach (var employee in saleCar.Employees)
                {
                    var existingEmployee = existingSaleCar.Employees.FirstOrDefault(e => e.Id == employee.Id);
                    if (existingEmployee == null)
                    {
                        existingSaleCar.Employees.Add(employee);
                    }
                    else
                    {
                        _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
                    }
                }

                // Update related BuyRow
                foreach (var buyRow in saleCar.BuyRow)
                {
                    var existingBuyRow = existingSaleCar.BuyRow.FirstOrDefault(b => b.Id == buyRow.Id);
                    if (existingBuyRow == null)
                    {
                        existingSaleCar.BuyRow.Add(buyRow);
                    }
                    else
                    {
                        _context.Entry(existingBuyRow).CurrentValues.SetValues(buyRow);
                    }
                }

                // Handle deleted related Cars
                foreach (var existingCar in existingSaleCar.Cars.ToList())
                {
                    if (!saleCar.Cars.Any(c => c.Id == existingCar.Id))
                    {
                        existingSaleCar.Cars.Remove(existingCar);
                    }
                }

                // Handle deleted related Employees
                foreach (var existingEmployee in existingSaleCar.Employees.ToList())
                {
                    if (!saleCar.Employees.Any(e => e.Id == existingEmployee.Id))
                    {
                        existingSaleCar.Employees.Remove(existingEmployee);
                    }
                }

                // Handle deleted related BuyRow
                foreach (var existingBuyRow in existingSaleCar.BuyRow.ToList())
                {
                    if (!saleCar.BuyRow.Any(b => b.Id == existingBuyRow.Id))
                    {
                        existingSaleCar.BuyRow.Remove(existingBuyRow);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var saleCar = await _context.SaleCars.FindAsync(id);
            if (saleCar != null)
            {
                _context.SaleCars.Remove(saleCar);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}