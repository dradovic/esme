using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace esme.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Circle> Circles { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Membership>()
                .HasKey(e => new { e.CircleId, e.UserId });

            modelBuilder.Entity<Circle>()
                .HasData(new Circle
                {
                    Id = Circle.OpenCircleId,
                    Name = "Open Circle",
                });

            modelBuilder.Entity<Message>()
                .HasIndex(m => m.CircleId);
        }
    }
}