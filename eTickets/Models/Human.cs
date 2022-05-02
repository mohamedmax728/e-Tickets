using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Human:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is Required")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "FullName")]
        [Required(ErrorMessage = " FullName is Required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Full Name mustbe between 3 and 50 chars")]
        public string FullName { get; set; }
        [Display(Name ="Biography")]
        [Required(ErrorMessage = " Biography is Required")]
        public string Bio { get; set; }

    }
}
