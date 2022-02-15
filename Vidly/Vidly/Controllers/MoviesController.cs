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
        public ViewResult Index()
        {
            var movies = new List<Movie>
            {
                new Movie {Name = "Matrix 1", Id=1},
                new Movie {Name = "Matrix 2", Id=2}
            };
            var viewModel = new ListMovieViewModel
            {
                Movies = movies,
            };
            return View(viewModel);
        }

        [Route("Movies/{Id:int}")]
        public ActionResult Movie(int Id)
        {
            var movies = new List<Movie>
            {
                new Movie {Name = "Matrix 1", Id=1},
                new Movie {Name = "Matrix 2", Id=2}
            };
            var foundMovie = movies.Find(movie => movie.Id == Id);

            //devolver vista de error
            if (foundMovie == null)
                return HttpNotFound();

            return View(foundMovie);
        }

    }
}