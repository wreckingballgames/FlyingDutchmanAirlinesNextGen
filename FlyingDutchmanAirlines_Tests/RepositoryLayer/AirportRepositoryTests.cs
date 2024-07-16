using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer;

[TestClass]
public class AirportRepositoryTests
{
    private FlyingDutchmanAirlinesContext _context;
    private AirportRepository _repository;

    [TestInitialize]
    public async Task TestInitialize()
    {
        DbContextOptions<FlyingDutchmanAirlinesContext>
        dbContextOptions = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
                .UseInMemoryDatabase("FlyingDutchman").Options;
        _context = new FlyingDutchmanAirlinesContext_Stub(dbContextOptions);

        Airport testAirport = new("Dallas", "DFW");
        _context.Airports.Add(testAirport);
        await _context.SaveChangesAsync();

        _repository = new(_context);
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    public async Task GetAirportByID_Success()
    {
        Airport airport = await _repository.GetAirportByID(0);
        Assert.IsNotNull(airport);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetAirportByID_Failure_InvalidInput()
    {
        await _repository.GetAirportByID(-1);
    }
}