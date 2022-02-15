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
                new Movie {Name = "Matrix 1", id=1},
                new Movie {Name = "Matrix 2", id=2}
            };
            var viewModel = new ListMovieViewModel
            {
                Movies = movies,
            };
            return View(viewModel);
        }

        [Route("Movies/{id:int}")]
        public ActionResult Movie(int id)
        {
            var movies = new List<Movie>
            {
                new Movie {Name = "Matrix 1", id=1},
                new Movie {Name = "Matrix 2", id=2}
            };
            var foundMovie = movies.Find(movie => movie.id == id);

            //devolver vista de error
            if (foundMovie == null)
                return HttpNotFound();

            return View(foundMovie);
        }

    }
}