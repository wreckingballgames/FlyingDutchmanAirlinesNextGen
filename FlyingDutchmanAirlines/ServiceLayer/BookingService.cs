using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchmanAirlines.ServiceLayer;

public class BookingService
{
    private readonly BookingRepository _repository;

    public BookingService(BookingRepository repository)
    {
        _repository = repository;
    }
}