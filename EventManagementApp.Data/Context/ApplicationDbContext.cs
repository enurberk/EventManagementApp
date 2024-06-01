using EventManagementApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementApp.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>(ConfigureEvent);
            base.OnModelCreating(builder);
        }
        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(ci => ci.Id);
            builder.Property(cb => cb.Title);
            builder.Property(cb => cb.Location);
            builder.Property(cb => cb.Time);
            builder.Property(cb => cb.IsFree);
            builder.Property(cb => cb.Price);
            builder.Property(cb => cb.Description);
            builder.Property(cb => cb.Image);
            builder.Property(cb => cb.EventType);
        }
    }
}
