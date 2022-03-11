using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public Order Order { get; set; }
    }
}