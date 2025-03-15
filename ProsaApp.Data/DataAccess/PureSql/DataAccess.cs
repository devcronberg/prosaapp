using System.Data;

using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace ProsaApp.Data.DataAccess.PureSql;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// ✅ Safe method: Uses parameterized queries to prevent SQL injection.
    /// Finds a customer by name.
    /// </summary>
    /// <param name="name">The name of the customer to find.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing the customer data.</returns>
    public DataTable FindCustomerByName(string name, ILogger logger)
    {
        logger.LogInformation("Trying to find customer by name: {Name}", name);
        return GetData("SELECT * FROM Customers WHERE Name LIKE @name",
            new Dictionary<string, object> { { "@name", $"%{name}%" } },
            logger);
    }

    /// <summary>
    /// ✅ Safe method: Uses parameterized queries to prevent SQL injection.
    /// Finds a customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to find.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing the customer data.</returns>
    public DataTable FindCustomerById(int id, ILogger logger)
    {
        logger.LogInformation("Trying to find customer by id: {Id}", id);
        return GetData("SELECT * FROM Customers WHERE Id = @id",
            new Dictionary<string, object> { { "@id", id } },
            logger);
    }

    /// <summary>
    /// ✅ Safe method: Uses parameterized queries to prevent SQL injection.
    /// Gets all customers from the database.
    /// </summary>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing all customer data.</returns>
    public DataTable GetAllCustomers(ILogger logger)
    {
        logger.LogInformation("Trying to get all customers from database");
        return GetData("SELECT * FROM Customers", null, logger);
    }

    /// <summary>
    /// ⚠️ Unsafe method: Potential SQL Injection risk due to string interpolation.
    /// Finds a customer by name using an unsafe query.
    /// Try name = "' OR '1'='1"; to see the SQL injection in action.
    /// </summary>
    /// <param name="name">The name of the customer to find.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing the customer data.</returns>
    public DataTable FindCustomerByNameBad(string name, ILogger logger)
    {
        logger.LogWarning("⚠️ UNSAFE SQL QUERY! Potential SQL Injection risk.");
        return GetData($"SELECT * FROM Customers WHERE Name LIKE '%{name}%'", logger);
    }

    /// <summary>
    /// ⚠️ Unsafe method: Potential SQL Injection risk due to string interpolation.
    /// Finds a customer by ID using an unsafe query.
    /// </summary>
    /// <param name="id">The ID of the customer to find.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing the customer data.</returns>
    public DataTable FindCustomerByIdBad(int id, ILogger logger)
    {
        logger.LogWarning("⚠️ UNSAFE SQL QUERY! Potential SQL Injection risk.");
        return GetData($"SELECT * FROM Customers WHERE Id = {id}", logger);
    }

    /// <summary>
    /// ✅ Safe method: Uses parameterized queries to prevent SQL injection.
    /// Executes a SQL query and returns the result as a DataTable.
    /// </summary>
    /// <param name="sql">The SQL query to execute.</param>
    /// <param name="parameters">The parameters for the SQL query.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing the query result.</returns>
    private DataTable GetData(string sql, Dictionary<string, object>? parameters, ILogger logger)
    {
        var dataTable = new DataTable();

        try
        {
            logger.LogInformation("Executing SQL query: {Query}", sql);
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            // Tilføj parametre sikkert
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            using var reader = command.ExecuteReader();
            dataTable.Load(reader);

            logger.LogInformation("SQL query executed successfully: {Count} rows found", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing SQL query: {Query}", sql);
        }

        return dataTable;
    }

    /// <summary>
    /// ⚠️ Unsafe method: Potential SQL Injection risk due to string interpolation.
    /// Executes a SQL query and returns the result as a DataTable.
    /// </summary>
    /// <param name="sql">The SQL query to execute.</param>
    /// <param name="logger">The logger to log information and errors.</param>
    /// <returns>A DataTable containing the query result.</returns>
    private DataTable GetData(string sql, ILogger logger)
    {
        var dataTable = new DataTable();

        try
        {
            logger.LogInformation("Trying to execute SQL query: {Query}", sql);
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            dataTable.Load(reader);

            logger.LogInformation("SQL query executed successfully: {Count} rows found", dataTable.Rows.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing SQL query: {Query}", sql);
        }

        return dataTable;
    }
}
