using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Movies_Rental.Models;
using Movies_Rental.ViewModels;

namespace Movies_Rental.Controllers
{
    public class CustomersController : Controller
    {
        private readonly DbContainer dbContainer;

        public CustomersController()
        {
            dbContainer = new DbContainer();
        }

        // GET: Customers
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = dbContainer.Customer.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = dbContainer.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = dbContainer.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            try
            {
                if (customer.Id == 0)
                    dbContainer.Customer.Add(customer);
                else
                {
                    var oldCustomer = dbContainer.Customer.Single(c => c.Id == customer.Id);

                    oldCustomer.Name = customer.Name;
                    oldCustomer.Birthdate = customer.Birthdate;
                    oldCustomer.MembershipTypeId = customer.MembershipTypeId;
                    oldCustomer.IsSubscribed = customer.IsSubscribed;
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
            var customer = dbContainer.Customer.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = dbContainer.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            dbContainer.Dispose();
        }

    }
}