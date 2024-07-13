using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class BookingRepository
{
    private readonly FlyingDutchmanAirlinesContext _context;

    public BookingRepository(FlyingDutchmanAirlinesContext context)
    {
        _context = context;
    }

    public async Task CreateBooking(int customerID, int flightNumber)
    {
        if (customerID < 0 || flightNumber < 0)
        {
            Console.WriteLine($"Argument Exception in CreateBooking! CustomerID = {customerID}, flightNumber = {flightNumber}");
            throw new ArgumentException("Invalid arguments provided");
        }

        Booking newBooking = new Booking
        {
            CustomerId = customerID,
            FlightNumber = flightNumber,
        };
    }
}