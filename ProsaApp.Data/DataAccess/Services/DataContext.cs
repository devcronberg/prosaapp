using System.Data;
using Microsoft.Extensions.Logging;

namespace ProsaApp.Data.DataAccess.Services;

public class DataContext
{

    private readonly IDataAccess dataAccess;

    public DataContext(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    public DataTable GetAllCustomers(ILogger logger)
    {
        return dataAccess.GetAllCustomers(logger);
    }
}