using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Producer:Human
    {
        //relationships
        public List<Movie> Movies { get; set; }
    }
}
