using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entity.ModelData;
using Entity.ModelData.Behaviors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Database_EFC.Persistence
{
    public class CarSharingDbContext : DbContext
    {
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=abul.db.elephantsql.com;" +
                "Database=bsovfjmv;" +
                "Username=bsovfjmv;" +
                "Password=31tBntzmwwOtrEMeGqAPJKk6VBGFI7CH;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Listing>().HasQueryFilter(p => !p.IsDeleted);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var changed = ChangeTracker.Entries();
            if (changed == null) return base.SaveChangesAsync(cancellationToken);

            UpdateSoftDeleteStatuses(changed);
            return base.SaveChangesAsync(cancellationToken);
        }
        
        private void UpdateSoftDeleteStatuses(IEnumerable<EntityEntry> changedEntries)
        {
            foreach (var entry in changedEntries)
            {
                if (entry.Entity is ISoftDeletable deletable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            deletable.IsDeleted = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            deletable.IsDeleted = true;
                            break;
                    }
                }
            }
        }
    }
}