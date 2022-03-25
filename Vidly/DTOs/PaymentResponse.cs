using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class PaymentResponse
    {
        public bool Valid { get; set; }
        public string Error { get; set; }

        public PaymentResponse(bool valid, string error)
        {
            Valid = valid;
            Error = error;
        }
    }
}