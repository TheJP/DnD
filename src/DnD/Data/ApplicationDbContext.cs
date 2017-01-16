using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DnD.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using DnD.Models.CharacterViewModels;

namespace DnD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<AdventureParticipation> AdventureParticipations { get; set; }
        public DbSet<DnDAttribute> Attributes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Gold> Gold { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<RaceAttribute> RaceAttributes { get; set; }

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
            builder.Entity<DnDAttribute>()
                .Property(a => a.Special)
                .HasDefaultValue(DnDAttribute.Specials.None);

            builder.Entity<Character>()
                .HasOne(c => c.Race)
                .WithMany(r => r.Characters)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Adventure>()
                .HasOne(a => a.DungeonMaster)
                .WithMany(u => u.Adventures)
                .OnDelete(DeleteBehavior.Restrict); //Don't delete all adventures, when deleting a user
            builder.Entity<Adventure>()
                .HasOne(a => a.Next)
                .WithOne(a => a.Previous)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Adventure>()
                .Property(a => a.Date)
                .HasDefaultValueSql("getdate()");

            //Gold and experience loot from an adventure have to be deleted before
            //the Adventure / AdventureParticipation can be deleted!
            builder.Entity<Gold>()
                .Property(g => g.Date)
                .HasDefaultValueSql("getdate()");

            builder.Entity<Experience>()
                .Property(e => e.Date)
                .HasDefaultValueSql("getdate()");
        }
    }
}
