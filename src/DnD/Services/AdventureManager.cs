﻿using DnD.Data;
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
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while creating adventure: {0}", e.Message); }
        }

        public async Task UpdateAsync(Adventure adventure)
        {
            try
            {
                context.Update(adventure);
                await context.SaveChangesAsync();
            }
            catch (Exception e) { logger.LogError("Exception while updating adventure: {0}", e.Message); }
        }
    }
}