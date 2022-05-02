using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropDownsVM> newMovieDropDownsVM();
        Task AddNewMovieAsync(NewMovieVM newMovieVM);
        Task UpdateMovieAsync(NewMovieVM movieVM);
    }
}
