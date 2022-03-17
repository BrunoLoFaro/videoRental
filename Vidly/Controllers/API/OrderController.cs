using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class OrderController : ApiController
    {
        private ApplicationDbContext _context;

        public OrderController()
        {
            _context = new ApplicationDbContext();
        }

       /* [HttpPost]
        public IHttpActionResult CreateOrder(OrderDto orderDto)
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

            var movies = _context.Movies.Where(
                m => orderDto.MovieIdsList.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberInStock == 0)
                    return BadRequest("Movie is not available.");

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
            catch(DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return Ok(order.Id);
        }*/

        [HttpPut]//add auth?
        public IHttpActionResult UpdateOrder(OrderDto orderDto)
        {
            var order = _context.Orders.Single(
                c => c.Id == orderDto.Id);

            order.CardId = orderDto.CardId;
            order.Price = 0;//query items table and sum

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return Ok(order.Id);
        }

    }
}
