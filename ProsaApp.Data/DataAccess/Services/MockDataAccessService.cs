using System.Data;
using Microsoft.Extensions.Logging;

namespace ProsaApp.Data.DataAccess.Services;
public class MockDataAccessService : IDataAccess
{
    public DataTable GetAllCustomers(ILogger logger)
    {
        // Mock implementation for testing purposes
        var dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));
        dataTable.Columns.Add("Country", typeof(string));
        dataTable.Columns.Add("Revenue", typeof(decimal));
        dataTable.Columns.Add("CreatedDate", typeof(DateTime));
        dataTable.Columns.Add("IsActive", typeof(bool));
        dataTable.Columns.Add("Tags", typeof(string));

        // Add mock data
        for (int i = 1; i <= 5; i++)
        {
            var row = dataTable.NewRow();
            row["Id"] = i;
            row["Name"] = $"Customer {i}";
            row["Age"] = 20 + i;
            row["Country"] = "Country " + i;
            row["Revenue"] = 1000 + (i * 100);
            row["CreatedDate"] = DateTime.Now.AddDays(-i);
            row["IsActive"] = true;
            row["Tags"] = $"Tag1, Tag2, Tag3";
            dataTable.Rows.Add(row);
        }

        return dataTable;
    }
}