using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Vidly.Dtos;
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
        /*public void func(Res)
        {
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Employee list  
                response = JsonConvert.DeserializeObject<PaymentResponse>(EmpResponse);

            }
            else
            {
                response = respuesta;
            }
        }*/
        [HttpGet]
        public async Task myFunc()
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "http://localhost:8080/card/first";
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                PaymentResponse response;
                PaymentResponse respuesta = new PaymentResponse(false,"failed to fetch");
                HttpResponseMessage Res = await client.GetAsync("");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    response = JsonConvert.DeserializeObject<PaymentResponse>(EmpResponse);
                }
                else
                {
                    response = respuesta;
                }

                int a = 2;
                int b = 2;
                int c = 2;
            }
            using (var client2 = new HttpClient())
            {
                string Baseurl2 = "http://localhost:8080/card/second";
                //Passing service base url  
                client2.BaseAddress = new Uri(Baseurl2);

                client2.DefaultRequestHeaders.Clear();
                //Define request data format  
                client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                await client2.GetAsync("");
            }
        }

        [HttpPost]
        public IHttpActionResult CreateOrder()
        {
            Task.Run(myFunc);
            return Ok();
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
