using GameReview.Domain.Core;
using GameReview.Domain.Models;
using GameReview.Domain.Models.Enumerations;
using GameReview.Infrastructure.Mappings;
using GameReview.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace GameReview.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameGender> GameGenders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new EnumerationMap<GameGender>());
            modelBuilder.ApplyConfiguration(new EnumerationMap<UserRole>());

            modelBuilder
                .Entity<GameGender>()
                .HasData(Enumeration.GetAll<GameGender>());

            modelBuilder
                .Entity<UserRole>()
                .HasData(Enumeration.GetAll<UserRole>());

            modelBuilder
                .Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    UserName = "admin",
                    Password = PasswordHasher.Hash("admin"),
                    Email = "admin@api.com",
                    Name = "Admin Root Application",
                    CreatedAt = DateTime.ParseExact("12/12/2012", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UserRoleId = UserRole.Admin.Id
                });
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null || entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("UpdatedAt").CurrentValue = null;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
