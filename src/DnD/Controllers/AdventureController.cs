using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnD.Data;
using DnD.Models;

namespace DnD.Controllers
{
    public class AdventureController : Controller
    {
        private readonly ApplicationDbContext context;

        public AdventureController(ApplicationDbContext context)
        {
            this.context = context;    
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Adventures
                .Include(a => a.DungeonMaster)
                .Include(a => a.Previous)
                .Include(a => a.Next);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await context.Adventures.SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        public IActionResult Create()
        {
            ViewData["DungeonMasters"] = new SelectList(context.Users, "Id", "DisplayName");
            ViewData["Adventures"] = new SelectList(context.Adventures, "Id", "Name");
            return View();
        }

        // POST: Adventure/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Description,DungeonMasterId,Name,NextId,PreviousId")] Adventure adventure)
        {
            if (ModelState.IsValid)
            {
                context.Add(adventure);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DungeonMasterId"] = new SelectList(context.Users, "Id", "Id", adventure.DungeonMasterId);
            ViewData["NextId"] = new SelectList(context.Adventures, "Id", "DungeonMasterId", adventure.NextId);
            return View(adventure);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await context.Adventures.SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null)
            {
                return NotFound();
            }
            ViewData["DungeonMasterId"] = new SelectList(context.Users, "Id", "Id", adventure.DungeonMasterId);
            ViewData["NextId"] = new SelectList(context.Adventures, "Id", "DungeonMasterId", adventure.NextId);
            return View(adventure);
        }

        // POST: Adventure/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description,DungeonMasterId,Name,NextId,PreviousId")] Adventure adventure)
        {
            if (id != adventure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(adventure);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventureExists(adventure.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["DungeonMasterId"] = new SelectList(context.Users, "Id", "Id", adventure.DungeonMasterId);
            ViewData["NextId"] = new SelectList(context.Adventures, "Id", "DungeonMasterId", adventure.NextId);
            return View(adventure);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await context.Adventures.SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adventure = await context.Adventures.SingleOrDefaultAsync(m => m.Id == id);
            context.Adventures.Remove(adventure);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AdventureExists(int id)
        {
            return context.Adventures.Any(e => e.Id == id);
        }
    }
}
