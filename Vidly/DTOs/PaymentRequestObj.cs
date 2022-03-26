using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class PaymentRequestObj
    {
        public long Id { get; set; }
        public float Price { get; set; }

        public PaymentRequestObj(long id, float price)
        {
            Id = id;
            Price = price;
        }
    }
}