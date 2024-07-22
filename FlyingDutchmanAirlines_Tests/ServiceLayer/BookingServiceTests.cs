using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.ServiceLayer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FlyingDutchmanAirlines_Tests.ServiceLayer;

[TestClass]
public class BookingServiceTests
{
    private Mock<CustomerRepository> _mockCustomerRepository;
    private Mock<BookingRepository> _mockBookingRepository;
    private BookingService _bookingService;

    [TestInitialize]
    public async Task TestInitialize()
    {
        _mockCustomerRepository = new Mock<CustomerRepository>();
        _mockCustomerRepository.Setup(repository =>
                repository.GetCustomerByName("Leo Tolstoy")).Returns(Task.FromResult(new Customer("Leo Tolstoy")));
        _mockBookingRepository = new Mock<BookingRepository>();
        _mockBookingRepository.Setup(repository =>
                repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);

        _bookingService = new BookingService(_mockCustomerRepository.Object, _mockBookingRepository.Object);
    }

    [TestMethod]
    public async Task CreateBooking_Success()
    {
        (bool result, Exception exception) = await _bookingService.CreateBooking("Leo Tolstoy", 0);

        Assert.IsTrue(result);
        Assert.IsNull(exception);
    }
}