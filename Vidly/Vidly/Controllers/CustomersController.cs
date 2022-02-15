using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }   

        [Route("Customers")]
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            var viewModel = new ListCustomerViewModel
            {
                Customers = customers,
            };
            return View(viewModel);
        }

        [Route("Customers/{Id:int}")]
        public ActionResult Customer(int Id)
        {
            var foundCustomer = _context.Customers.SingleOrDefault(c => c.Id == Id);

            //devolver vista de error
            if (foundCustomer == null)
                return HttpNotFound();

            return View(foundCustomer);
        }
    }
}