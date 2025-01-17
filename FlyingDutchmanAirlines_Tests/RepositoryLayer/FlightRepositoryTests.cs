using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer;

[TestClass]
public class FlightRepositoryTests
{
    private FlyingDutchmanAirlinesContext _context;
    private FlightRepository _repository;

    [TestInitialize]
    public async Task TestInitialize()
    {
        DbContextOptions<FlyingDutchmanAirlinesContext>
        dbContextOptions = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;
        _context = new FlyingDutchmanAirlinesContext_Stub(dbContextOptions);

        Flight testFlight = new();
        testFlight.FlightNumber = 1;
        testFlight.Origin = 1;
        testFlight.Destination = 2;
        _context.Flights.Add(testFlight);
        await _context.SaveChangesAsync();

        _repository = new FlightRepository(_context);
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    public async Task GetFlightByFlightNumber_Success()
    {
        Flight testFlight = await _repository.GetFlightByFlightNumber(1, 1, 2);
        Assert.IsNotNull(testFlight);
        Assert.AreEqual(testFlight.FlightNumber, 1);
        Assert.AreEqual(testFlight.Origin, 1);
        Assert.AreEqual(testFlight.Destination, 2);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetFlightByFlightNumber_Failure_InvalidOriginAirportId()
    {
        await _repository.GetFlightByFlightNumber(0, -1, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetFlightByFlightNumber_Failure_InvalidDestinationAirportId()
    {
        await _repository.GetFlightByFlightNumber(0, 0, -1);
    }

    [TestMethod]
    [ExpectedException(typeof(FlightNotFoundException))]
    public async Task GetFlightByFlightNumber_Failure_InvalidFlightNumber()
    {
        await _repository.GetFlightByFlightNumber(-1, 0, 0);
    }
}