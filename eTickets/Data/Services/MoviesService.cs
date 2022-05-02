using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Price = data.Price,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                ImageUrl = data.ImageUrl,
                Description = data.Description,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,
                CinemaId = data.CinemaId
            };
            await _context.movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();
            //add Movies Actors
            foreach (var item in data.Actorids)
            {
                var l = new Actor_Movie()
                {
                    ActorId = item,
                    MovieId = newMovie.Id
                };
                await _context.actor_Movies.AddAsync(l);
            }

            await _context.SaveChangesAsync();

        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var Movie = await _context.movies
                .Include(p => p.producer)
                .Include(c => c.cinema)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            return Movie;
        }

        public async Task<NewMovieDropDownsVM> newMovieDropDownsVM()
        {
            var response = new NewMovieDropDownsVM()
            {
                actors = await _context.actors.OrderBy(n => n.FullName).ToListAsync(),
                producers = await _context.producers.OrderBy(n => n.FullName).ToListAsync(),
                cinemas = await _context.cinemas.OrderBy(n => n.Name).ToListAsync()
            };



            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {

                dbMovie.Name = data.Name;
                dbMovie.Price = data.Price;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.ImageUrl = data.ImageUrl;
                dbMovie.Description = data.Description;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                dbMovie.CinemaId = data.CinemaId;

                await _context.SaveChangesAsync();
            }
            //Remove existing actors
            var ExistingActorsdb = _context.actor_Movies.Where(n => n.MovieId == data.Id).ToList();
             _context.actor_Movies.RemoveRange(ExistingActorsdb);
            await _context.SaveChangesAsync();
            //add Movies Actors
            foreach (var item in data.Actorids)
            {
                var l = new Actor_Movie()
                {
                    ActorId = item,
                    MovieId = data.Id
                };
                await _context.actor_Movies.AddAsync(l);
            }

            await _context.SaveChangesAsync();

        }
    }
}

