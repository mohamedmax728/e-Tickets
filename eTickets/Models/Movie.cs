using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Movie:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        //relationships
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema cinema { get; set; }
        public int ProducerId { get; set; }
        public Producer producer { get; set; }
        public List<Actor_Movie> Actor_Movies { get; set; }

    }
}
