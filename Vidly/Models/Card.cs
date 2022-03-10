using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Brand { get; set; }
        public byte Code { get; set; }
        public string Owner { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}