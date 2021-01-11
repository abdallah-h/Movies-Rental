using AutoMapper;
using Movies_Rental.Dtos;
using Movies_Rental.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Movies_Rental.Controllers.API
{
    public class MoviesController : ApiController
    {
        private DbContainer dbContainer;

        public MoviesController()
        {
            dbContainer = new DbContainer();
        }

        public IHttpActionResult GetMovies()
        {
            var movieDto = dbContainer.Movie
                .Include(c => c.Genre).ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDto);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = dbContainer.Movie.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            dbContainer.Movie.Add(movie);
            dbContainer.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = dbContainer.Movie.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);

            dbContainer.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = dbContainer.Movie.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();

            dbContainer.Movie.Remove(movieInDb);
            dbContainer.SaveChanges();

            return Ok();
        }
    }
}
