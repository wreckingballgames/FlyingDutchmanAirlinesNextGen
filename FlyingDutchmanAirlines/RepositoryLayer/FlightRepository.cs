using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class FlightRepository
{
    private readonly FlyingDutchmanAirlinesContext _context;

    public FlightRepository(FlyingDutchmanAirlinesContext context)
    {
        _context = context;
    }

    public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId)
    {
        if (!flightNumber.IsPositive() || !originAirportId.IsPositive() || !destinationAirportId.IsPositive())
        {
            Console.WriteLine($"Argument Exception in GetFlightByFlightNumber! Flight number = {flightNumber}, origin airport ID = {originAirportId}, destination airport ID = {destinationAirportId}");
            throw new ArgumentException("Invalid arguments provided");
        }

        return await _context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
                ?? throw new FlightNotFoundException();
    }
}