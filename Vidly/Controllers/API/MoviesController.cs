using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    //[Authorize(Roles = RoleName.CanManageMovies)]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<MovieDTO> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie,MovieDTO>);
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if(movie == null)
                return BadRequest(); //return exception
            return Ok(Mapper.Map<Movie, MovieDTO>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); //return exception
            var movie = Mapper.Map<MovieDTO, Movie>(customerDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            customerDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), customerDto);
        }

        // PUT: api/Movies/5
        [HttpPut]
        public IHttpActionResult PutMovie(int id, MovieDTO customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customerInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

                _context.SaveChanges();

            return Ok(customerDto);
        }

        // DELETE: api/Movies/5
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var customerInDb = _context.Movies.SingleOrDefault(c=>c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _context.Movies.Remove(customerInDb);
            _context.SaveChanges();

            return Ok(customerInDb);
        }
    }
}