using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public byte CustomerId { get; set; }

        public int Price { get; set; }

        public byte CardId { get; set; }
    }
}