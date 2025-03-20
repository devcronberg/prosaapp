using System.Data;
using Microsoft.Extensions.Logging;
using ProsaApp.Data.DataAccess.PureSql;

namespace ProsaApp.Data.DataAccess.Services;

public class DataAccessService : IDataAccess
{
    private readonly string _connectionString;

    public DataAccessService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable GetAllCustomers(ILogger logger)
    {
        var dbHelper = new DatabaseHelper(_connectionString);
        return dbHelper.GetAllCustomers(logger);
    }

}