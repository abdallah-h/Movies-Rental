using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Movies_Rental.Models;
using Movies_Rental.ViewModels;

namespace Movies_Rental.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DbContainer dbContainer;

        public MoviesController()
        {
            dbContainer = new DbContainer();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var movie = dbContainer.Movie.Include(g => g.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = dbContainer.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = dbContainer.Genre.ToList()
                };
                return View("MovieForm", viewModel);
            }
            try
            {
                if (movie.Id == 0)
                {
                    movie.DateAdded = DateTime.Now;
                    dbContainer.Movie.Add(movie);
                }
                else
                {
                    var oldCustomer = dbContainer.Movie.Single(c => c.Id == movie.Id);

                    oldCustomer.Name = movie.Name;
                    oldCustomer.ReleaseDate = movie.ReleaseDate;
                    oldCustomer.GenreId = movie.GenreId;
                    oldCustomer.NumberInStock = movie.NumberInStock;
                }
                dbContainer.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public ActionResult Edit(int id)
        {
            var movie = dbContainer.Movie.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = dbContainer.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            dbContainer.Dispose();
        }
    }
}