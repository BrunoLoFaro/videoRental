﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class RentalDto
    {
        public int Id { get; set; }

        public byte CustomerId { get; set; }

        public List<byte> MovieIdsList { get; set; }

        public byte CardId { get; set; }

        public CardDto Card { get; set; }
    }
}