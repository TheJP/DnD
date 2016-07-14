using DnD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnD.Data
{
    public static class DataSeed
    {
        public static void Seed(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        }
    }
}
