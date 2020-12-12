using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies_Rental.Models;

namespace Movies_Rental.Controllers
{
    public class MoviesController : Controller
    {
        private DbContainer dbContainer;

        public MoviesController()
        {
            dbContainer = new DbContainer();
        }

        protected override void Dispose(bool disposing)
        {
            dbContainer.Dispose();
        }

        // GET: Movie
        public ActionResult Index()
        {
            var movies = dbContainer.Movie.Include(g => g.Genre).ToList();

            return View(movies);
        }

        [Route("Movies/Details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = dbContainer.Movie.Include(g => g.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
    }
}