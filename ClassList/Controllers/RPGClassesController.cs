﻿using System;
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

        
        public async Task<IActionResult> Index(string searchString, string sortOrder="")
        {
            /* Purpose:
             *  This function provides the list of RPGClasses to the View as well
             *  filter it using LINQ based on certain actions from the user
             */
            ViewData["CNSortParm"] = sortOrder == "name_D" ? "name_A" : "name_D";
            ViewData["HPSortParm"] = sortOrder == "hp_min" ? "hp_max" : "hp_min";
            ViewData["SPSortParm"] = sortOrder == "speed_min" ? "speed_max" : "speed_min";
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
                            c => c.SpecialAbility).ToList();
                    break;
                case "special_D":
                    classes = classes
                        .AsParallel().OrderByDescending(
                            c => c.SpecialAbility).ToList();
                    break;
                default:
                    break;

            }
            return View(classes);
        }


        public async Task<IActionResult> Details(int? id)
        {
            /* Purpose:
             *  This function provides the model data of an RPGClass based on
             *  the given id
             */
            var rPGClass = id != null ? await _context.RPGClass
                            .FirstOrDefaultAsync(m => m.Id == id) : null;
            if (rPGClass == null)
                return NotFound();
            return View(rPGClass);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,HealthPoints,Speed,SpecialAbility")] RPGClass rPGClass)
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
            /* Purpose:
             *  This function provides the model data of an RPGClass based on
             *  the given id
             */
            var rPGClass = id != null ? await _context.RPGClass
                            .FirstOrDefaultAsync(m => m.Id == id) : null;
            if (rPGClass == null)
                return NotFound();
            return View(rPGClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,HealthPoints,Speed,SpecialAbility")] RPGClass rPGClass)
        {
            /* Purpose:
             *  This function will save any changes made to model data as
             *  long as the changes are not invalid. If invalid, it will
             *  tell the user.
             */
            if (id != rPGClass.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rPGClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.RPGClass.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rPGClass);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            /* Purpose:
             *  This function provides the model data of an RPGClass based on
             *  the given id
             */
            var rPGClass = id != null ? await _context.RPGClass
                .FirstOrDefaultAsync(m => m.Id == id) : null;
            if (rPGClass == null)
                return NotFound();
            return View(rPGClass);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /* Purpose:
             *  This function will remove the specified object from the 
             *  database based on the given id
             */
            var rPGClass = await _context.RPGClass.FindAsync(id);
            _context.RPGClass.Remove(rPGClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
