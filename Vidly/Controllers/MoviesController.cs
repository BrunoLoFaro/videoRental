using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole(("CanManageMovies")))
                return View("IndexAdmin", movies);
            return View("Index", movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles= RoleName.CanManageMovies)]
        public ActionResult NewMovie()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new NewMovieViewModel
            {
                Genres = genres
            };
            return View(viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult Edit(int id)
        {

            var viewModel = new NewMovieViewModel
            {
                Genres = _context.Genres.ToList(),
                Movie = _context.Movies.Single(m=>m.Id == id)

            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel
                {
                    Genres = _context.Genres.ToList()
                };
                return View("NewMovie", viewModel);
            }
            if(movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.GenreId= movie.GenreId;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}