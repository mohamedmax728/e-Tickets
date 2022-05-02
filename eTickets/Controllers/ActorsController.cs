using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorsService;
        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }
        public async Task<IActionResult> Index()
        {
            var data =await _actorsService.GetAllAsync();
            return View(data);
        }
        //Get:Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
           await _actorsService.AddAsync(actor);
            return RedirectToAction(nameof(Index));
           
        }
        public async Task<IActionResult> Details(int id)
        {
            var ActorDetails =await _actorsService.GetAsync(id);
            if (ActorDetails == null)return View("NotFound");
            return View(ActorDetails);
            
        }
        //Get:Actors/Edit/1
        public async Task<IActionResult> EditAsync(int id)
        {
            var ActorDetails = await _actorsService.GetAsync(id);
            if (ActorDetails == null)
            {
                return View("NotFound");
            }
            return View(ActorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id,[Bind("Id,ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _actorsService.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));

        }
        //Get:Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ActorDetails = await _actorsService.GetAsync(id);
            if (ActorDetails == null)return View("NotFound");
            return View(ActorDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ActorDetails = await _actorsService.GetAsync(id);
            if (ActorDetails == null) return View("NotFound");

            await _actorsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
