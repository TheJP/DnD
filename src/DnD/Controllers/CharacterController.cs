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
            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), "Id", "Name");
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
            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), "Id", "Name", viewModel.RaceId);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }
            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            if (character == null) { return NotFound(); }

            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), "Id", "Name", character.RaceId);
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

            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), "Id", "Name", viewModel.RaceId);
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
        public async Task<IActionResult> AddGold(int id, int amount, string description, string from)
        {
            var participation = await context.AdventureParticipations.SingleAsync(ap => ap.Id == id);
            return RedirectToAction
            (
                "Details",
                from == "Adventure" ? "Adventure" : "Character",
                new { Id = (from == "Adventure" ? participation.AdventureId : participation.AdventurerId) }
            );
        }

        private bool CharacterExists(int id)
        {
            return context.Characters.Any(e => e.Id == id);
        }
    }
}
