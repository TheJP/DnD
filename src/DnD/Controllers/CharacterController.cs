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

namespace DnD.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly CharacterManager manager;

        public CharacterController(ApplicationDbContext context, CharacterManager manager)
        {
            this.context = context;
            this.manager = manager;
        }

        // GET: Character
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Characters.Include(c => c.Owner).Include(c => c.Race);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Character/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Character/Create
        public IActionResult Create()
        {
            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CharacterViewModel character)
        {
            if (ModelState.IsValid)
            {
                var newEntity = new Character()
                {
                    Name = character.Name,
                    Gender = character.Gender,
                    RaceId = character.RaceId,
                    OwnerId = User.Identity.Name
                };
                await manager.CreateAsync(newEntity);
                return RedirectToAction("Index");
            }
            ViewData["RaceItems"] = new SelectList(context.Races.OrderBy(r => r.Name), "Id", "Name", character.RaceId);
            return View(character);
        }

        // GET: Character/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(context.Users, "Id", "Id", character.OwnerId);
            ViewData["RaceId"] = new SelectList(context.Races, "Id", "Name", character.RaceId);
            return View(character);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Gender,Name,OwnerId,RaceId")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(character);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
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
            ViewData["OwnerId"] = new SelectList(context.Users, "Id", "Id", character.OwnerId);
            ViewData["RaceId"] = new SelectList(context.Races, "Id", "Name", character.RaceId);
            return View(character);
        }

        // GET: Character/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await context.Characters.SingleOrDefaultAsync(m => m.Id == id);
            context.Characters.Remove(character);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CharacterExists(int id)
        {
            return context.Characters.Any(e => e.Id == id);
        }
    }
}
