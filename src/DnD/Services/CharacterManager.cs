using DnD.Data;
using DnD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Services
{
    public class CharacterManager
    {
        private readonly ApplicationDbContext context;

        public CharacterManager(ApplicationDbContext context) { this.context = context; }

        public async Task CreateAsync(Character character)
        {
            context.Characters.Add(character);
            await context.SaveChangesAsync();
        }
    }
}
