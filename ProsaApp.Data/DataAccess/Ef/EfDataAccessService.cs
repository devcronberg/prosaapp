using System.Data;
using Microsoft.Extensions.Logging;
using ProsaApp.Data.DataAccess.Ef;
using ProsaApp.Data.DataAccess.Services;
using ProsaApp.Domain.Types;

namespace ProsaApp.Data.DataAccess.Ef;

public class EfDataAccessService : IDataAccess
{
    private readonly string _connectionString;

    public EfDataAccessService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Customer> GetAllCustomers(ILogger logger)
    {
        using EfContext context = new EfContext(_connectionString);
        logger.LogInformation("Getting all customers from database using EF Core using " + _connectionString);
        var customers = context.Customers.ToList();
        logger.LogInformation("Got {Count} customers from database", customers.Count);
        return customers;
    }
}