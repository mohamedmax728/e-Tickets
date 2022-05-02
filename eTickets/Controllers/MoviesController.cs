using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _Service;
        public MoviesController(IMoviesService Service)
        {
            _Service = Service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _Service.GetAllAsync(n=>n.cinema);
            return View(allMovies);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _Service.GetAllAsync(n => n.cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var result = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains
                (searchString)).ToList();
                return View("Index",result);
            }
            return View("Index", allMovies);
        }
        //Get:Movies/Details/1 
        public async Task<IActionResult> Details(int id)
        {
            var Movie =await _Service.GetMovieByIdAsync(id);
            if (Movie == null) return View("NotFound");
            return View(Movie);
        }
        //Get:Movies/Create
        public async Task<IActionResult> Create()
        {
            var moviedropdown =await _Service.newMovieDropDownsVM();
            ViewBag.Cinemas = new SelectList(moviedropdown.cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(moviedropdown.producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(moviedropdown.actors, "Id", "FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovieVM)
        {
            if (!ModelState.IsValid)
            {
                var moviedropdown = await _Service.newMovieDropDownsVM();
                ViewBag.Cinemas = new SelectList(moviedropdown.cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(moviedropdown.producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(moviedropdown.actors, "Id", "FullName");

                View(newMovieVM);
            }
            await _Service.AddNewMovieAsync(newMovieVM);
            return RedirectToAction(nameof(Index));
        }
        //Get:Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _Service.GetMovieByIdAsync(id);
            if (data == null) return View("NotFound");
            var response = new NewMovieVM()
            {
                Id = data.Id,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Description = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                MovieCategory = data.MovieCategory,
                Name = data.Name,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId,
                Actorids = data.Actor_Movies.Select(n => n.ActorId).ToList()
            };
            var moviedropdown = await _Service.newMovieDropDownsVM();
            ViewBag.Cinemas = new SelectList(moviedropdown.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(moviedropdown.producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(moviedropdown.actors, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM newMovieVM)
        {
            
            if (!ModelState.IsValid)
            {
                var moviedropdown = await _Service.newMovieDropDownsVM();
                ViewBag.Cinemas = new SelectList(moviedropdown.cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(moviedropdown.producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(moviedropdown.actors, "Id", "FullName");

                View(newMovieVM);
            }
            await _Service.UpdateMovieAsync(newMovieVM);
            return RedirectToAction(nameof(Index));
        }
    }
}
