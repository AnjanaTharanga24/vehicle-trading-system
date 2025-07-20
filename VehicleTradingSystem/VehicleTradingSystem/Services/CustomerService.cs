using VehicleTradingSystem.Data;
using VehicleTradingSystem.Models;

namespace VehicleTradingSystem.Services
{
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService()
        {
            _context = new AppDbContext();
        }

        public Customer Authenticate(string username, string password)
        {
            return _context.Customers
                .FirstOrDefault(c => c.Username == username && c.Password == password);
        }
        public bool AddCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCustomer(string customerId)
        {
            try
            {
                var customer = _context.Customers.Find(customerId);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    return _context.SaveChanges() > 0;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Customer GetCustomerById(string customerId)
        {
            return _context.Customers.Find(customerId);
        }
    }
}