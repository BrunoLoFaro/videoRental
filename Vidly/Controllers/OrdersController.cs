using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using Vidly.Dtos;
using Vidly.DTOs;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        static PaymentRequestObj reqObj = new PaymentRequestObj(0,0);
        static string plainTextContent;
        static string plainTextContent2;
        static string plainTextContent3;
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
                plainTextContent3 += movie.Name+",";
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

            var viewModel = new summaryViewModel
            {
                UserName = User.Identity.GetUserName(),
                MovieList = movies,
                Order = order
            };

            reqObj.Id = order.CardId;
            reqObj.Price = order.Price;
            plainTextContent += $"Card Id {reqObj.Id}\n";
            plainTextContent2 += $"Amount: ${reqObj.Price}";
            string combinedString = string.Join(",", movies);
            Task.Run(MakePayment);
            return View("summary", viewModel);
        }

        static async Task MakePayment()
        {
            using (var client = new HttpClient())
            {
                string Baseurl = "http://localhost:8080/card/payment";
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.Timeout = TimeSpan.FromMilliseconds(100);
                PaymentResponseObj response;
                PaymentResponseObj defResponse = new PaymentResponseObj(false, "failed to fetch");

                var stringPayload = JsonConvert.SerializeObject(reqObj);
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
            var subject = "Payment succesful";
            var to = new EmailAddress("lofarobruno@gmail.com", "Receiving User");
            var htmlContent = $"<strong>RDXMovies payment summary</strong><p>{plainTextContent}</p><p>{plainTextContent3}</p><p>{plainTextContent2}</p>";
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
    }
}