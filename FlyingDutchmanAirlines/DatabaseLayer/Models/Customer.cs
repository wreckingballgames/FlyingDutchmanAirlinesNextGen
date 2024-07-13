using System;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public sealed class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    public Customer(string name)
    {
        Bookings = new HashSet<Booking>();
        Name = name;
    }

        public static bool operator == (Customer x, Customer y)
    {
        CustomerEqualityComparer comparer = new();
        return comparer.Equals(x, y);
    }

    public static bool operator != (Customer x, Customer y)
    {
        return !(x == y);
    }
}

internal class CustomerEqualityComparer : EqualityComparer<Customer>
{
    public override int GetHashCode(Customer obj)
    {
        int randomNumber = RandomNumberGenerator.GetInt32(int.MaxValue / 2);
        return (obj.CustomerId + obj.Name.Length + randomNumber).GetHashCode();
    }

    public override bool Equals(Customer x, Customer y)
    {
        return x.CustomerId == y.CustomerId && x.Name == y.Name;
    }
}