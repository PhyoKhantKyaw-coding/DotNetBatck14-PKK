using DotNetBatch14PKK.LoanTrackerDatabase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.LoanTrackerDomain;

public class CustomerService
{
    private readonly AppDbContext _dbContext;
    public CustomerService()
    {
        _dbContext = new AppDbContext();
    }
    public ResponseModel CreateCustomer(CustomerModel customer)
    {
        customer.Id = Guid.NewGuid().ToString(); 
        _dbContext.Customer.Add(customer);
        var result = _dbContext.SaveChanges();
        return new ResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Customer created successfully" : "Failed to create customer"
        };
    }
    public List<CustomerModel> GetCustomers()
    {
        var customers = _dbContext.Customer.AsNoTracking().ToList();
        return customers;
    }
    public ResponseModel GetCustomerById(string id)
    {
        var customer = _dbContext.Customer.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (customer == null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "Customer not found"
            };
        }
        return new ResponseModel
        {
            IsSuccess = true,
            Message = "Customer found",
            Data = customer
        };
    }
}
