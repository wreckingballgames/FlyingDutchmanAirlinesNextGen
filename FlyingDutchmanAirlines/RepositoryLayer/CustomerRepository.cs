using System.Linq;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        public CustomerRepository(FlyingDutchmanAirlinesContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCustomer(string name)
        {
            if (IsInvalidCustomerName(name))
            {
                return false;
            }

            try
            {
                Customer newCustomer = new(name);

                using (_context)
                {
                    _context.Customers.Add(newCustomer);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            return new Customer(name);
        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = {'!', '@', '#', '$', '%', '&', '*'};
            return string.IsNullOrEmpty(name) || name.Any(x =>
                    forbiddenCharacters.Contains(x));
        }
    }
}