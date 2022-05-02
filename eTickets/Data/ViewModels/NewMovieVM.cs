using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie Name is required")]
        [DisplayName("Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Movie Description is required")]
        [DisplayName("Movie Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Movie price is required")]
        [DisplayName("Movie price in $")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Movie Poster Url is required")]
        [DisplayName("Movie Poster Url")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Movie Start Date is required")]
        [DisplayName("Movie Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Movie End Date is required")]
        [DisplayName("Movie End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie Category is required")]
        [DisplayName("Select Movie Category")]
        public MovieCategory MovieCategory { get; set; }
        //relationships
        [Required(ErrorMessage = "Actor(s) is required")]
        [DisplayName("Select Actor(s)")]
        public List<int> Actorids { get; set; }
        [Required(ErrorMessage = "Cinema is required")]
        [DisplayName("Select Cinema")]
        public int CinemaId { get; set; }
        [Required(ErrorMessage = "Producer is required")]
        [DisplayName("Select Producer")]
        public int ProducerId { get; set; }

    }
}
