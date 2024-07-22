using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class CustomerRepository
{
    private readonly FlyingDutchmanAirlinesContext _context;

    [MethodImpl(MethodImplOptions.NoInlining)]
    public CustomerRepository()
    {
        if (Assembly.GetExecutingAssembly().FullName ==
                Assembly.GetCallingAssembly().FullName)
        {
            throw new Exception("This constructor should only be used for testing");
        }
    }

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

    public virtual async Task<Customer> GetCustomerByName(string name)
    {
        if (IsInvalidCustomerName(name))
        {
            throw new CustomerNotFoundException();
        }

        return await _context.Customers.FirstOrDefaultAsync(c => c.Name == name)
                ?? throw new CustomerNotFoundException();
    }

    private bool IsInvalidCustomerName(string name)
    {
        char[] forbiddenCharacters = {'!', '@', '#', '$', '%', '&', '*'};
        return string.IsNullOrEmpty(name) || name.Any(x =>
                forbiddenCharacters.Contains(x));
    }
}