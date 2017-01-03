using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnD.Data;
using DnD.Models;
using DnD.Models.AdventureViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DnD.Services;

namespace DnD.Controllers
{
    [Authorize]
    public class AdventureController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly AdventureManager manager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdventureController(ApplicationDbContext context, AdventureManager manager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.manager = manager;
            this.userManager = userManager;
        }

        private Adventure AdventureFromViewModel(AdventureViewModel viewModel, Adventure adventure = null)
        {
            if(adventure == null) { adventure = new Adventure(); }
            adventure.Name = viewModel.Name;
            adventure.Description = viewModel.Description;
            adventure.Date = viewModel.Date ?? DateTime.Today; //TODO: Make Adventure.Date optional
            adventure.DungeonMasterId = viewModel.DungeonMasterId;
            adventure.PreviousId = viewModel.PreviousId;
            adventure.NextId = viewModel.NextId;
            return adventure;
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
            if (id == null) { return NotFound(); }
            var adventure = await context.Adventures
                .Include(a => a.DungeonMaster)
                .Include(a => a.Previous)
                .Include(a => a.Next)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null) { return NotFound(); }

            return View(adventure);
        }

        private void Create(string dungeonMaster, int? previous = null, int? next = null)
        {
            ViewData["DungeonMasters"] = new SelectList(context.Users.OrderBy(u => u.DisplayName), "Id", "DisplayName", dungeonMaster);
            var adventures = context.Adventures.OrderByDescending(a => a.Date);
            ViewData["Previous"] = new SelectList(adventures, "Id", "Name", previous);
            ViewData["Next"] = new SelectList(adventures, "Id", "Name", next);
        }

        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            Create(user.Id);
            return View();
        }

        // POST: Adventure/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Description,DungeonMasterId,Name,NextId,PreviousId")] AdventureViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newAdventure = AdventureFromViewModel(viewModel);
                await manager.CreateAsync(newAdventure);
                return RedirectToAction("Details", new { Id = newAdventure.Id });
            }
            Create(viewModel.DungeonMasterId, viewModel.PreviousId, viewModel.NextId);
            return View(viewModel);
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
