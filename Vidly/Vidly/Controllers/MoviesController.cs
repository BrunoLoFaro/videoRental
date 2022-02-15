using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        [Route("Movies")]
        public ActionResult Index()
        {
            var movies = new List<Movie>
            {
                new Movie {Name = "Movie 1"},
                new Movie {Name = "Movie 2"}
            };
            var viewModel = new ListMovieViewModel
            {
                Movies = movies,
            };
            return View(viewModel);
        }

        [Route("Movies/{id:int}")]
        public ActionResult Enum(int id)
        {
            return Content("id"+id);
        }

        //viewModel ex
        [Route("Movies/Random")]
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek"};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }
        //constraint ex
        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year+"/"+month);
        }
    }
}