using System;
using System.Collections.Generic;
using System.Linq;
using Vidly.Models;
using System.Web;

namespace Vidly.ViewModels
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes;
        public Customer Customer;
    }
}