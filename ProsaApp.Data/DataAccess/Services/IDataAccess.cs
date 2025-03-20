using System.Data;
using Microsoft.Extensions.Logging;

namespace ProsaApp.Data.DataAccess.Services;

public interface IDataAccess
{
    DataTable GetAllCustomers(ILogger logger);
}