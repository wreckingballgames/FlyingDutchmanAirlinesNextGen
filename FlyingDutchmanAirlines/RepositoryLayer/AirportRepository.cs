using System.Net.Mime;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class AirportRepository
{
    private readonly FlyingDutchmanAirlinesContext _context;

    public AirportRepository(FlyingDutchmanAirlinesContext context)
    {
        _context = context;
    }

    public async Task<Airport> GetAirportByID(int airportID)
    {
        if (airportID < 0)
        {
            Console.WriteLine($"Argument Exception in GetAirportByID! AirportID = {airportID}");
            throw new ArgumentException("Invalid argument provided");
        }

        return await _context.Airports.FirstOrDefaultAsync(a => a.AirportId == airportID)
                ?? throw new AirportNotFoundException();
    }
}