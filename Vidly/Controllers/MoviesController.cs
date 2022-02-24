using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
                Genres = genres,
            };
            return View("MovieForm",viewModel);
        }
        
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult Save(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);
            var viewModel = new NewMovieViewModel(movie)
            {
                Genres = _context.Genres.ToList(),
            };
            return View("MovieForm",viewModel);
        }

        //http delete
        public ActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                Trace.WriteLine("El modelo no era valido" + movie);
                var viewModel = new NewMovieViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            Trace.WriteLine("pasó la validation");
            if(movie.Id == 0){
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
                Trace.WriteLine("guardada");
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Genre = movie.Genre;
                movieInDb.GenreId= movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}