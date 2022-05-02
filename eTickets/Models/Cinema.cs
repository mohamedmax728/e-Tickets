using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Cinema Logo")]
        [Required(ErrorMessage = "Description is required")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Description is required")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Description mustbe between 3 and 50 chars")]
        public string Description { get; set; }
        //relationships
        public List<Movie> Movies { get; set; }
    }
}
