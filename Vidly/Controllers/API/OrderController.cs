using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using Vidly.Dtos;
using Vidly.DTOs;
using Vidly.Migrations;
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
        public IHttpActionResult CreateOrder()
        {

            Task.Run(MakePayment);

            return Ok();

        }

        static async Task MakePayment()
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "http://localhost:8080/card/";
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = TimeSpan.FromMilliseconds(100);
                PaymentResponseObj response;
                PaymentResponseObj defResponse = new PaymentResponseObj(false, "failed to fetch");
                PaymentRequestObj obj = new PaymentRequestObj(134245666464, 500);
                var stringPayload = JsonConvert.SerializeObject(obj);
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage Res = await client.PostAsync("", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var empResponse = Res.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<PaymentResponseObj>(empResponse);
                        if (response.Valid)
                        {
                            await SendMail();
                        }
                    }
                    else
                    {
                        response = defResponse;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"exception{e}");
                }
            }
        }

        static async Task SendMail()
        {
            var apiKey = Environment.GetEnvironmentVariable("EMAIL_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("lofarobruno@gmail.com", "Sending User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("lofarobruno@gmail.com", "Receiving User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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
         }
         */
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
