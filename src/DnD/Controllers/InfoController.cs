using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DnD.Data;
using Microsoft.EntityFrameworkCore;

namespace DnD.Controllers
{
    public class InfoController : Controller
    {
        private ApplicationDbContext context;

        public InfoController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var model = context.Races
                .Include(r => r.Attributes)
                .ThenInclude(a => a.Attribute)
                .OrderBy(r => r.Name)
                .ToList();
            return View(model);
        }
    }
}