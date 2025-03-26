using System.Data;
using Microsoft.Extensions.Logging;
using ProsaApp.Data.DataAccess.Ef;
using ProsaApp.Data.DataAccess.Mock;

namespace ProsaApp.Data.DataAccess.Services;

public class DataAccessFactory
{
    public static IDataAccess CreateDataContext(string connectionString)
    {
        // if DEBUG...
        // return new MockDataAccessService();
        // else if RELEASE...
        return new EfDataAccessService(connectionString);
    }
}