using AutoMapper;
using Movies_Rental.Dtos;
using Movies_Rental.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Movies_Rental.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly DbContainer dbContainer;

        public CustomersController()
        {
            dbContainer = new DbContainer();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customerDtos = dbContainer.Customer
                .Include(c => c.MembershipType).ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = dbContainer.Customer.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            dbContainer.Customer.Add(customer);
            dbContainer.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = dbContainer.Customer.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            dbContainer.SaveChanges();

            return Ok();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = dbContainer.Customer.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            dbContainer.Customer.Remove(customerInDb);
            dbContainer.SaveChanges();

            return Ok();
        }
    }
}
