using System.Data;
using Microsoft.Extensions.Logging;
using ProsaApp.Domain.Types;

namespace ProsaApp.Data.DataAccess.Services;

public interface IDataAccess
{
    IEnumerable<Customer> GetAllCustomers(ILogger logger);

}