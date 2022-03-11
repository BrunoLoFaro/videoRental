using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
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

        [HttpPost]
        public IHttpActionResult CreateOrder(OrderDto orderDto)
        {
            var customer = _context.Customers.Single(
                c => c.Id == orderDto.CustomerId);
            
                var order = new Order
                {
                    Customer = customer,
                    CardId = orderDto.CardId,
                    Price = 0
                };
        
            _context.Orders.Add(order);

            try
            {
                _context.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return Ok(order.Id);
        }
    }
}
