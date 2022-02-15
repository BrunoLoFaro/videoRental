using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        [Route("Customers")]
        public ViewResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer {Name = "Agent Smith", Id=1},
                new Customer {Name = "Neo", Id=2}
            };
            var viewModel = new ListCustomerViewModel
            {
                Customers = customers,
            };
            return View(viewModel);
        }

        [Route("Customers/{Id:int}")]
        public ActionResult Customer(int Id)
        {
            var customers = new List<Customer>
            {
                new Customer {Name = "Agent Smith", Id=1},
                new Customer {Name = "Neo", Id=2}
            };
            var foundCustomer = customers.Find(customer => customer.Id == Id);

            //devolver vista de error
            if (foundCustomer == null)
                return HttpNotFound();

            return View(foundCustomer);
        }
    }
}