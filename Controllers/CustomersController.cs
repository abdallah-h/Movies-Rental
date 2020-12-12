using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Movies_Rental.Models;

namespace Movies_Rental.Controllers
{
    public class CustomersController : Controller
    {
        private DbContainer dbContainer;

        public CustomersController()
        {
            dbContainer = new DbContainer();
        }

        

        // GET: Customers
        public ActionResult Index()
        {
            var customers = dbContainer.Customer.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        [Route("Customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = dbContainer.Customer.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            dbContainer.Dispose();
        }

    }
}