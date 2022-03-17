using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }

        public List<int> MovieIdsList { get; set; }

        public byte CardId { get; set; }//byte?
    }
}