
// Define AppSettings class
public class AppSettings
{
    public ConnectionStrings? ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public string? DefaultConnection { get; set; } = "";
}
