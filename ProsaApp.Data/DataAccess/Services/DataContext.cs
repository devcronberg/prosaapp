using System.Data;
using Microsoft.Extensions.Logging;
using ProsaApp.Domain.Types;

namespace ProsaApp.Data.DataAccess.Services;

public class DataContext
{

    private readonly IDataAccess dataAccess;

    public DataContext(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    public IEnumerable<Customer> GetAllCustomers(ILogger logger)
    {
        return dataAccess.GetAllCustomers(logger);
    }
}