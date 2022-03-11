using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public List<int> MovieIdsList { get; set; }
        public byte OrderId { get; set; }
    }
}