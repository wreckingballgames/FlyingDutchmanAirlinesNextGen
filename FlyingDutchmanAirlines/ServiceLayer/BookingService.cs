using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchmanAirlines.ServiceLayer;

public class BookingService
{
    private readonly CustomerRepository _customerRepository;
    private readonly BookingRepository _bookingRepository;

    public BookingService(CustomerRepository customerRepository, BookingRepository bookingRepository)
    {
        _customerRepository = customerRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<(bool, Exception)> CreateBooking(
            string name, int flightNumber)
    {
        try
        {
            Customer customer;
            try
            {
                customer = await _customerRepository.GetCustomerByName(name);
            }
            catch (CustomerNotFoundException)
            {
                await _customerRepository.CreateCustomer(name);
                return await CreateBooking(name, flightNumber);
            }

            await _bookingRepository.CreateBooking(customer.CustomerId, flightNumber);
            return (true, null);
        }
        catch (Exception exception)
        {
            return (false, exception);
        }
    }
}