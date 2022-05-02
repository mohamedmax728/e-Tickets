using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Actor:Human
    {
        //relationships
        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}
