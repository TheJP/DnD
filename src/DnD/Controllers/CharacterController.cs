using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnD.Data;
using DnD.Models;
using DnD.Models.CharacterViewModels;
using DnD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DnD.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly CharacterManager manager;
        private readonly UserManager<ApplicationUser> userManager;

        public CharacterController(ApplicationDbContext context, CharacterManager manager, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.manager = manager;
            this.userManager = userManager;
        }

        private async Task<Character> CharacterFromViewModel(CharacterViewModel viewModel, Character character = null)
        {
            if (character == null) { character = new Character(); }
            character.Name = viewModel.Name;
            character.Gender = viewModel.Gender;
            character.RaceId = viewModel.RaceId;
            character.Owner = await userManager.GetUserAsync(User);
            character.InitialLife = viewModel.InitialLife;
            character.InitialMana = viewModel.InitialMana;
            return character;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Characters
                .Include(c => c.Owner)
                .Include(c => c.Race);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }
            var character = await context.Characters
                .Include(c => c.Owner)
                .Include(c => c.Race)
                .Include(c => c.Gold)
                .Include(c => c.Experience)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (character == null) { return NotFound(); }

            return View(character);
        }

        public IActionResult Create()
        {
            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), nameof(Race.Id), nameof(Race.Name));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CharacterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newCharacter = await CharacterFromViewModel(viewModel);
                await manager.CreateAsync(newCharacter);
                return RedirectToAction("Details", new { Id = newCharacter.Id });
            }
            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), nameof(Race.Id), nameof(Race.Name), viewModel.RaceId);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }
            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            if (character == null) { return NotFound(); }

            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), nameof(Race.Id), nameof(Race.Name), character.RaceId);
            return View(new CharacterViewModel()
            {
                Id = character.Id,
                Gender = character.Gender,
                Name = character.Name,
                RaceId = character.RaceId,
                InitialLife = character.InitialLife,
                InitialMana = character.InitialMana
            });
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CharacterViewModel viewModel)
        {
            if (!viewModel.Id.HasValue || id != viewModel.Id.Value) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    await manager.UpdateAsync(await CharacterFromViewModel(viewModel, await context.Characters.SingleAsync(c => c.Id == id)));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction("Details", new { Id = id });
            }

            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), nameof(Race.Id), nameof(Race.Name), viewModel.RaceId);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var character = await context.Characters
                .Include(c => c.Race)
                .Include(c => c.Owner)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (character == null) { return NotFound(); }

            return View(character);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            context.Characters.Remove(character);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGold(AddGoldViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Prepare new Gold entity
                var newGold = new Gold()
                {
                    Value = viewModel.Amount,
                    Description = viewModel.Description,
                    CharacterId = viewModel.AdventurerId
                };
                if (viewModel.AdventureId.HasValue)
                {
                    var participation = await context.AdventureParticipations
                        .SingleAsync(ap => ap.AdventureId == viewModel.AdventureId && ap.AdventurerId == viewModel.AdventurerId);
                    newGold.LootFromId = participation.Id;
                }

                //Add Gold to database
                await manager.AddGoldAsync(newGold);

                //Redirect back to character or adventure
                var viewAdventure = viewModel.From == "Adventure" && viewModel.AdventureId.HasValue;
                return RedirectToAction
                (
                    "Details", viewAdventure ? "Adventure" : "Character",
                    new { Id = (viewAdventure ? viewModel.AdventureId.Value : viewModel.AdventurerId) }
                );
            }
            var adventures = context.Adventures
                .OrderByDescending(a => a.Date)
                .Select(a => new { Id = a.Id, Name = $"{a.Name} ({a.Date:d})" });
            ViewData["Adventures"] = new SelectList(adventures, "Id", "Name", viewModel.AdventureId);
            ViewData["Adventurers"] = new SelectList(context.Characters.OrderBy(c => c.Name), nameof(Adventure.Id), nameof(Adventure.Name), viewModel.AdventureId);
            return View(viewModel);
        }

        private bool CharacterExists(int id)
        {
            return context.Characters.Any(e => e.Id == id);
        }
    }
}
