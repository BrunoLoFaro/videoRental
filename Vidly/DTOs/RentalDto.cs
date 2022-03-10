using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class RentalDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public List<int> MovieIds{ get; set; }

        public int CardId { get; set; }
    }
}