using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DnD.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DnD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<DnDAttribute> Attributes { get; set; }
        public DbSet<RaceAttribute> RaceAttributes { get; set; }
        public DbSet<Race> Races { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DnDAttribute>()
                .Property(a => a.Type)
                .HasDefaultValue(DnDAttribute.LevelUp.WithCharacterPoints);
            builder.Entity<Character>()
                .HasOne(c => c.Race)
                .WithMany(r => r.Characters)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
