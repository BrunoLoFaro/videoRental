using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class PaymentResponseObj
    {
        public bool Valid { get; set; }
        public string Error { get; set; }

        public PaymentResponseObj(bool valid, string error)
        {
            Valid = valid;
            Error = error;
        }
    }
}