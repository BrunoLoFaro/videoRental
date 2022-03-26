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

        public string MovieIdsList { get; set; }//sent as a json

        public long CardId { get; set; }//byte?
    }
}