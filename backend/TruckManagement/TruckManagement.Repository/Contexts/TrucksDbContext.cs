using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckManagement.Models.Entities;
using TruckManagement.Models.Entities.Base;

namespace TruckManagement.Repository.Contexts
{
    public class TrucksDbContext : DbContext
    {
        public TrucksDbContext(DbContextOptions<TrucksDbContext> options) : base(options)
        {
        }

        public DbSet<Truck> Trucks { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}