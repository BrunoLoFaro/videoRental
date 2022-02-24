using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageCustomers)]
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

        public ViewResult Index()
        {
            var customers = _context.Customers.Include(m => m.MembershipType).ToList();
            if(User.IsInRole(("CanManageCustomers")))
                return View("IndexAdmin", customers);
            return View("Index", customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(m => m.MembershipType).SingleOrDefault(m => m.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult NewCustomer()
        {
            var genres = _context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = genres,
            };
            return View("CustomerForm",viewModel);
        }
        
        public ViewResult Save(int id)
        {
            var customer = _context.Customers.Single(m => m.Id == id);
            var viewModel = new NewCustomerViewModel(customer)
            {
                MembershipTypes = _context.MembershipTypes.ToList(),
            };
            return View("CustomerForm",viewModel);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var customerInDb = _context.Customers.Single(m => m.Id == id);
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel(customer)
                {
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0){
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(m => m.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.MembershipTypeId= customer.MembershipTypeId;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}