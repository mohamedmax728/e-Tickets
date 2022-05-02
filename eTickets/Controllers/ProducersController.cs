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
    public class ProducersController : Controller
    {
        private readonly IProducersService _producersService;
        public ProducersController(IProducersService producersService)
        {
            _producersService=producersService;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _producersService.GetAllAsync();
            return View(allProducers);
        }
        //Get:producer/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var Producer = await _producersService.GetAsync(id);
            if (Producer == null)
            {
                return View("NotFound");
            }
            return View(Producer);
        }
        //Get:Producer/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")]Producer newProducer)
        {
            if(!ModelState.IsValid)
            {
                return View(newProducer);
            }
            await _producersService.AddAsync(newProducer);
            return RedirectToAction(nameof(Index));
        }
        //Get:producer/Edit/1
        public async Task< IActionResult> Edit(int id)
        {
            var Producer = await _producersService.GetAsync(id);
            if (Producer == null) return View("NotFound");
            return View(Producer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,ProfilePictureUrl,FullName,Bio")]Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            if(id==producer.Id)
            {
                await _producersService.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }
        //Get:producer/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _producersService.GetAsync(id);
            if (producer == null) return View("NotFound");
            return View(producer);

        }
        [HttpPost,ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmation(int id)
        {

            await _producersService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
