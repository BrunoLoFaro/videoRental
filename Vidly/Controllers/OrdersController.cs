using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Orders
        public ActionResult NewOrder()
        {
            return View();
        }

        public ActionResult Save(OrderDto orderDto)
        {

            var currentUserId = User.Identity.GetUserId();

            var order = new Order
            {
                UserId = currentUserId,
                CardId = orderDto.CardId,
                Price = 0,
                IsValid = false
            };

            _context.Orders.Add(order);

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            List<int> ids = new List<int>();
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic dynamicList = js.Deserialize<dynamic>(orderDto.MovieIdsList);
            foreach (var item in dynamicList)
            {
                ids.Add(item);
            }


            var movies = _context.Movies.Where(
                m => ids.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberInStock == 0)
                    return HttpNotFound("Movie is not available.");

                movie.NumberInStock--;
                order.Price += movie.Price;

                var item = new Item
                {
                    Movie = movie,
                    Order = order
                };

                _context.Item.Add(item);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return View("summary",order);
        }
    }
}