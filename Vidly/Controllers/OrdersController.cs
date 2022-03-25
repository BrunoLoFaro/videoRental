using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Vidly.Dtos;
using Vidly.DTOs;
using Vidly.Models;
using Vidly.ViewModels;

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

        public async Task<ActionResult> Save(OrderDto orderDto)
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

            var viewModel = new summaryViewModel
            {
                UserName = User.Identity.GetUserName(),
                MovieList = movies,
                Order = order
            };

            using (var client = new HttpClient())
            {
                PaymentResponse response;
                PaymentResponse respuesta = new PaymentResponse(false,"failed to fetch");
                string Baseurl = "http://localhost:8080/card/payment";
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("");

                //Checking the response is successful or not which is sent using HttpClient  
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
            }

            return View("summary", viewModel);
        }
    }
}