using DnD.Data;
using DnD.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DnD.Services
{
    public class CharacterManager
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public CharacterManager(ApplicationDbContext context, ILoggerFactory factory)
        {
            this.context = context;
            this.logger = factory.CreateLogger<CharacterManager>();
        }

        /// <summary>Set life and mana to default values, if none were chosen.</summary>
        private async Task SetLifeAndMana(Character character)
        {
            if(
                //No values selected or
                character.InitialLife == 0 ||
                //Chosen race is dragon
                (await context.Races.SingleAsync(r => r.Id == character.RaceId)).IsDragon
            ){
                //Set default values for initial life and mana
                var defaults = await context.Races
                    .Where(r => r.Id == character.RaceId)
                    .Select(r => new {
                        Life = r.Attributes.Single(a => a.Attribute.Special == DnDAttribute.Specials.Life).Value,
                        Mana = r.Attributes.Single(a => a.Attribute.Special == DnDAttribute.Specials.Mana).Value
                    })
                    .SingleAsync();
                character.InitialLife = defaults.Life;
                character.InitialMana = defaults.Mana;
            }
        }

        public async Task CreateAsync(Character character)
        {
            try
            {
                await SetLifeAndMana(character);
                context.Characters.Add(character);
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while creating character: {0}", e.Message); }
        }

        public async Task UpdateAsync(Character character)
        {
            try
            {
                await SetLifeAndMana(character);
                context.Update(character);
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while updating character: {0}", e.Message); }
        }
    }
}
