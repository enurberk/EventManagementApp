using EventManagementApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementApp.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>(ConfigureEvent);
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
