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
    public class AdventureManager
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger logger;

        public AdventureManager(ApplicationDbContext context, ILoggerFactory factory)
        {
            this.context = context;
            this.logger = factory.CreateLogger<CharacterManager>();
        }

        public async Task CreateAsync(Adventure adventure)
        {
            try
            {
                context.Adventures.Add(adventure);
                //TODO: Handle Next and Previous correctly
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while creating adventure: {0}", e.Message); }
        }

        public async Task UpdateAsync(Adventure adventure)
        {
            try
            {
                context.Update(adventure);
                //TODO: Handle Next and Previous correctly
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while updating adventure: {0}", e.Message); }
        }

        public async Task AddAdventurers(int adventure, IEnumerable<int> adventurers)
        {
            try
            {
                var participations = adventurers.Select(adventurerId => new AdventureParticipation()
                {
                    AdventureId = adventure,
                    AdventurerId = adventurerId
                });
                context.AdventureParticipations.AddRange(participations);
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while adding adventurers: {0}", e.Message); }
        }
    }
}
