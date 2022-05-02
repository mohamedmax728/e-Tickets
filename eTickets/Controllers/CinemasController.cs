using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;
        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await  _cinemasService.GetAllAsync();
            return View(allCinemas);
        }
        //Get:Cinema/Create 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost,ActionName("Create")]
        public async Task<IActionResult> CreateConfirmation([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if(!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _cinemasService.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        //Get:Cinema/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var cinema=await _cinemasService.GetAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        //Get:Cinema/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _cinemasService.GetAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost,ActionName("Edit")]
        public async Task<IActionResult> EditConfirmation(int id, [Bind("Id,Logo,Name,Description")]Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _cinemasService.UpdateAsync(id,cinema);
            return RedirectToAction(nameof(Index));
        }
        //Get:Cinema/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinema =await _cinemasService.GetAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            
            await _cinemasService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
