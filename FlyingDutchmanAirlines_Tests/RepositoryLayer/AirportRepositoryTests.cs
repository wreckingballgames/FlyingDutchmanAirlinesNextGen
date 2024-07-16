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
        Assert.AreEqual(0, airport.AirportId);
        Assert.AreEqual("Dallas", airport.City);
        Assert.AreEqual("DFW", airport.Iata);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetAirportByID_Failure_InvalidInput()
    {
        StringWriter outputStream = new();
        try
        {
            Console.SetOut(outputStream);
            await _repository.GetAirportByID(-1);
        }
        catch(ArgumentException)
        {
            Assert.IsTrue(outputStream.ToString().Contains("Argument Exception in GetAirportByID! AirportID = -1"));
            throw new ArgumentException();
        }
        finally
        {
            outputStream.Dispose();
        }
    }

    [TestMethod]
    [ExpectedException(typeof(AirportNotFoundException))]
    public async Task GetAirportByID_Failure_InvalidAirportID()
    {
        await _repository.GetAirportByID(100);
    }
}