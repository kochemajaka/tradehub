using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System;
using tradehub.Domain.Model;

namespace tradehub.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<BuyCar> BuyCars { get; set; }
        public DbSet<BuyoutCar> BuyoutCars { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<DDS> DDSs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PrepareCar> PrepareCars { get; set; }
        public DbSet<SaleCar> SaleCars { get; set; }
        public DbSet<Job> Jobs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}