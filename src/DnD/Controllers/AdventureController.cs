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

        private void PrepareSelectLists(string dungeonMasterId, int? previousId = null, int? nextId = null)
        {
            ViewData["DungeonMasters"] = new SelectList(context.Users.OrderBy(u => u.DisplayName), nameof(ApplicationUser.Id), nameof(ApplicationUser.DisplayName), dungeonMasterId);
            var adventures = context.Adventures
                .OrderByDescending(a => a.Date)
                .Select(a => new { Id = a.Id, Name = $"{a.Name} ({a.Date:d})" });
            ViewData["Previous"] = new SelectList(adventures, "Id", "Name", previousId);
            ViewData["Next"] = new SelectList(adventures, "Id", "Name", nextId);
        }

        private void PrepareAdventurersSelect(int adventureId, IEnumerable<int> selected = null)
        {
            var characters = context.Characters
                .Where(c => !c.Adventures.Any(ap => ap.AdventureId == adventureId))
                .OrderBy(c => c.Name);
            ViewData["Characters"] = new MultiSelectList(characters, nameof(Character.Id), nameof(Character.Name), selected);
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Adventures
                .Include(a => a.DungeonMaster)
                .OrderByDescending(a => a.Date);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }
            var adventure = await context.Adventures
                .Include(a => a.DungeonMaster)
                .Include(a => a.Previous)
                .Include(a => a.Next)
                .Include(a => a.Adventurers)
                    .ThenInclude(ap => ap.Adventurer)
                    .ThenInclude(c => c.Race)
                .Include(a => a.Adventurers)
                    .ThenInclude(ap => ap.GoldLoot)
                .Include(a => a.Adventurers)
                    .ThenInclude(ap => ap.ExperienceLoot)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null) { return NotFound(); }
            PrepareAdventurersSelect(id.Value);

            return View(adventure);
        }

        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            PrepareSelectLists(user.Id);
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
            PrepareSelectLists(viewModel.DungeonMasterId, viewModel.PreviousId, viewModel.NextId);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }
            var adventure = await context.Adventures.SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null) { return NotFound(); }

            PrepareSelectLists(adventure.DungeonMasterId, adventure.PreviousId, adventure.NextId);
            return View(new AdventureViewModel()
            {
                Id = adventure.Id,
                Name = adventure.Name,
                Date = adventure.Date,
                DungeonMasterId = adventure.DungeonMasterId,
                Description = adventure.Description,
                PreviousId = adventure.PreviousId,
                NextId = adventure.NextId
            });
        }

        // POST: Adventure/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description,DungeonMasterId,Name,NextId,PreviousId")] AdventureViewModel viewModel)
        {
            if (!viewModel.Id.HasValue || id != viewModel.Id.Value) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    await manager.UpdateAsync(AdventureFromViewModel(viewModel, await context.Adventures.SingleAsync(a => a.Id == id)));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventureExists(id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction("Details", new { Id = id });
            }

            PrepareSelectLists(viewModel.DungeonMasterId, viewModel.PreviousId, viewModel.NextId);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var adventure = await context.Adventures
                .Include(a => a.DungeonMaster)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (adventure == null) { return NotFound(); }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdventurers(AddAdventurersViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await manager.AddAdventurers(viewModel.AdventureId, viewModel.Adventurers);
                return RedirectToAction("Details", new { Id = viewModel.AdventureId });
            }
            PrepareAdventurersSelect(viewModel.AdventureId, viewModel.Adventurers);
            ViewData["Adventures"] = new SelectList(context.Adventures.OrderByDescending(a => a.Date), nameof(Adventure.Id), nameof(Adventure.Name), viewModel.AdventureId);
            return View(viewModel);
        }

        private bool AdventureExists(int id)
        {
            return context.Adventures.Any(e => e.Id == id);
        }
    }
}
