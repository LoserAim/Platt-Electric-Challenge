using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassList.Data;
using ClassList.Models;
using System.ComponentModel;

namespace ClassList.Controllers
{
    public class RPGClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RPGClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RPGClasses
        public async Task<IActionResult> Index(string searchString, string sortOrder="")
        {
            ViewData["CNSortParm"] = sortOrder == "name_D" ? "name_A" : "name_D";
            ViewData["HPSortParm"] = sortOrder == "hp_min" ? "hp_max" : "hp_min";
            ViewData["SpeedSortParm"] = sortOrder == "speed_min" ? "speed_max" : "speed_min";
            ViewData["SASortParm"] = sortOrder == "special_D" ? "special_A" : "special_D";
            ViewData["CurrentFilter"] = searchString;
            var classes = await _context.RPGClass.ToListAsync();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                classes = classes.AsParallel().Where(c =>
                            c.ClassName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "name_A":
                    classes = classes
                        .AsParallel().OrderBy(
                            c => c.ClassName).ToList();
                    break;
                case "name_D":
                    classes = classes
                        .AsParallel().OrderByDescending(
                            c => c.ClassName).ToList();
                    break;
                case "hp_min":
                    classes = classes
                        .AsParallel().OrderBy(
                            c => c.HealthPoints).ToList();
                    break;
                case "hp_max":
                    classes = classes
                        .AsParallel().OrderByDescending(
                            c => c.HealthPoints).ToList();
                    break;
                case "speed_max":
                    classes = classes
                        .AsParallel().OrderBy(
                        c => c.Speed).ToList();
                    break;
                case "speed_min":
                    classes = classes
                        .AsParallel().OrderByDescending(
                            c => c.Speed).ToList();
                    break;
                case "special_A":
                    classes = classes
                        .AsParallel().OrderBy(
                            c => c.SpecialAbilities).ToList();
                    break;
                case "special_D":
                    classes = classes
                        .AsParallel().OrderByDescending(
                            c => c.SpecialAbilities).ToList();
                    break;
                default:
                    break;

            }
            return View(classes);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rPGClass = await _context.RPGClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rPGClass == null)
            {
                return NotFound();
            }

            return View(rPGClass);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,HealthPoints,Speed,SpecialAbilities")] RPGClass rPGClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rPGClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rPGClass);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rPGClass = await _context.RPGClass.FindAsync(id);
            if (rPGClass == null)
            {
                return NotFound();
            }
            return View(rPGClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,HealthPoints,Speed,SpecialAbilities")] RPGClass rPGClass)
        {
            if (id != rPGClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rPGClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RPGClassExists(rPGClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rPGClass);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rPGClass = await _context.RPGClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rPGClass == null)
            {
                return NotFound();
            }

            return View(rPGClass);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rPGClass = await _context.RPGClass.FindAsync(id);
            _context.RPGClass.Remove(rPGClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool RPGClassExists(int id)
        {
            return _context.RPGClass.Any(e => e.Id == id);
        }
    }
}
