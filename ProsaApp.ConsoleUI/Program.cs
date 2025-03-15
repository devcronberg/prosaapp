using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var appSettings = new AppSettings();
        configuration.Bind(appSettings);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddSerilog();
        });

        ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

        logger.LogInformation("App start");
        // should not be in appsettings.json but in a secret store (environment variables, Azure Key Vault, etc.)
        if (appSettings.ConnectionStrings?.DefaultConnection == null)
        {
            logger.LogError("ConnectionStrings:DefaultConnection is missing in appsettings.json");
            return;
        }
        string connectionString = appSettings.ConnectionStrings.DefaultConnection;
        
        var dbHelper = 
            new ProsaApp.Data.DataAccess.PureSql.DatabaseHelper(connectionString);
        string name = "' OR '1'='1";
        DataTable customers = dbHelper.FindCustomerByNameBad(name, logger);

        foreach (DataRow row in customers.Rows)
        {
            logger.LogInformation("Customer: {Id} {Name} {Age} {Country} {Revenue} {CreatedDate} {IsActive} {Tags}",
                row["Id"], row["Name"], row["Age"], row["Country"], row["Revenue"], row["CreatedDate"], row["IsActive"], string.Join(", ", row["Tags"]));
        }
    }
}

// Define AppSettings class
public class AppSettings
{
    public ConnectionStrings? ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public string? DefaultConnection { get; set; } = "";
}
