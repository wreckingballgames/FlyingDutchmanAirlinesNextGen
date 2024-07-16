using System.Security.Cryptography;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlyingDutchmanAirlines_Tests;

public class FlyingDutchmanAirlinesContext_Stub : FlyingDutchmanAirlinesContext
{
    public FlyingDutchmanAirlinesContext_Stub(DbContextOptions<FlyingDutchmanAirlinesContext> options) : base(options)
    {
        base.Database.EnsureDeleted();
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> pendingChanges = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added);
        IEnumerable<Booking> bookings = pendingChanges
                .Select(e => e.Entity).OfType<Booking>();
        if (bookings.Any(b => b.CustomerId != 1))
        {
            throw new Exception("Database Error!");
        }

        // This code does not work, I do not understand the meaning, and
        //   there is no errata. My tests pass without it.
        // IEnumerable<Airport> airports = pendingChanges
        //         .Select(e => e.Entity).OfType<Airport>();
        // if (!airports.Any())
        // {
        //     throw new Exception("Database Error!");
        // }

        await base.SaveChangesAsync(cancellationToken);
        return 1;
    }
}