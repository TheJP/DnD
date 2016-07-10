using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Models
{
    public class DnDContext : DbContext
    {
        public DnDContext(DbContextOptions<DnDContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
    }
}
