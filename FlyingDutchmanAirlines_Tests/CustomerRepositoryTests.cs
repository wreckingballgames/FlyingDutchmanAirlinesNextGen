using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer;

[TestClass]
public class CustomerRepositoryTests
{
    [TestMethod]
    public void CreateCustomer_Success()
    {
        CustomerRepository repository = new();
        Assert.IsNotNull(repository);

        bool result = repository.CreateCustomer("Donald Knuth");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CreateCustomer_Failure_NameIsNull()
    {
        CustomerRepository repository = new();
        Assert.IsNotNull(repository);

        bool result = repository.CreateCustomer(null);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateCustomer_Failure_NameIsEmptyString()
    {
        CustomerRepository repository = new();
        Assert.IsNotNull(repository);

        bool result = repository.CreateCustomer(string.Empty);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow('#')]
    [DataRow('$')]
    [DataRow('%')]
    [DataRow('&')]
    [DataRow('*')]
    public void CreateCustomer_Failure_NameContainsInvalidCharacters(char invalidCharacter)
    {
        CustomerRepository repository = new();
        Assert.IsNotNull(repository);

        bool result = repository.CreateCustomer("Donald Knuth" + invalidCharacter);
        Assert.IsFalse(result);
    }
}