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
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }   

        [Route("Movies")]
        public ViewResult Index()
        {
            var movies = _context.Movies.ToList();
            var viewModel = new ListMovieViewModel
            {
                Movies = movies,
            };
            return View(viewModel);
        }

        [Route("Movies/{Id:int}")]
        public ActionResult Movie(int Id)
        {
            var foundMovie = _context.Movies.SingleOrDefault(c => c.Id == Id);

            //devolver vista de error
            if (foundMovie == null)
                return HttpNotFound();

            return View(foundMovie);
        }

    }
}