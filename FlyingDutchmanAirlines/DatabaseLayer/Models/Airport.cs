using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public partial class Airport
{
    public int AirportId { get; set; }

    public string City { get; set; } = null!;

    public string Iata { get; set; } = null!;

    public virtual ICollection<Flight> FlightDestinationNavigations { get; set; }

    public virtual ICollection<Flight> FlightOriginNavigations { get; set; }

    public Airport(string city, string iata)
    {
        City = city;
        Iata = iata;
        FlightDestinationNavigations = new List<Flight>();
        FlightOriginNavigations = new List<Flight>();
    }
}
