using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProsaApp.Data.DataAccess.Services;
using Serilog;

class Program
{
    static void Main()
    {

        // Set up configuration sources.
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var appSettings = new AppSettings();
        configuration.Bind(appSettings);

        // Set up Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSerilog();
        });
        ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation("App start");
        logger.LogInformation("Getting connection string from appsettings.json");
        // should not be in appsettings.json but in a secret store (environment variables, Azure Key Vault, etc.)
        if (appSettings.ConnectionStrings?.DefaultConnection == null)
        {
            logger.LogError("ConnectionStrings:DefaultConnection is missing in appsettings.json");
            return;
        }
        string connectionString = appSettings.ConnectionStrings.DefaultConnection;
        logger.LogInformation("Connection string: {ConnectionString}", connectionString);


        logger.LogInformation("Getting all customers from database using pure SQL");
        var dbHelper =
            new ProsaApp.Data.DataAccess.PureSql.DatabaseHelper(connectionString);
        DataTable customers = dbHelper.GetAllCustomers(logger);
        foreach (DataRow row in customers.Rows)
        {
            logger.LogInformation("Customer: {Id} {Name} {Age} {Country} {Revenue} {CreatedDate} {IsActive} {Tags}",
                row["Id"], row["Name"], row["Age"], row["Country"], row["Revenue"], row["CreatedDate"], row["IsActive"], string.Join(", ", row["Tags"]));
        }

        logger.LogInformation("Getting customers by name from database using pure SQL");
        string name = "Anders";
        //string name = "' OR '1'='1"; // try this to see if SQL injection works
        DataTable customer = dbHelper.FindCustomerByNameBad(name, logger);
        foreach (DataRow row in customer.Rows)
        {
            logger.LogInformation("Customer: {Id} {Name} {Age} {Country} {Revenue} {CreatedDate} {IsActive} {Tags}",
                row["Id"], row["Name"], row["Age"], row["Country"], row["Revenue"], row["CreatedDate"], row["IsActive"], string.Join(", ", row["Tags"]));
        }

        logger.LogInformation("Getting customers by name from database a context");
        var context = DataAccessFactory.CreateDataContext(connectionString);
        customers = context.GetAllCustomers(logger);
        foreach (DataRow row in customers.Rows)
        {
            logger.LogInformation("Customer: {Id} {Name} {Age} {Country} {Revenue} {CreatedDate} {IsActive} {Tags}",
                row["Id"], row["Name"], row["Age"], row["Country"], row["Revenue"], row["CreatedDate"], row["IsActive"], string.Join(", ", row["Tags"]));
        }

        logger.LogInformation("App end");
    }
}

