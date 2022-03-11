using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class ItemsController : ApiController
    {
        private ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRental(ItemDto itemDto)
        {

            var movies = _context.Movies.Where(
                m => itemDto.MovieIdsList.Contains(m.Id)).ToList();

            var order = _context.Orders.Single(
                            c => c.Id == itemDto.OrderId);


             foreach (var movie in movies)
             {
                 if (movie.NumberInStock == 0)
                     return BadRequest("Movie is not available.");
                 movie.NumberInStock--;

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

             return Ok();
        }
    }
}
