using System.Data;
using Microsoft.Extensions.Logging;

namespace ProsaApp.Data.DataAccess.Services;

public class DataAccessFactory
{
    public static IDataAccess CreateDataContext(string connectionString)
    {
        // if DEBUG...
        return new MockDataAccessService();
        // else if RELEASE...
        //return new DataAccessService(connectionString);
    }
}