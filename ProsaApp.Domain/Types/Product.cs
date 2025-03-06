namespace ProsaApp.Domain.Types;

/// <summary>
/// Represents a product in the inventory.
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Parameterless constructor required by EF Core.
    /// </summary>
    private Product() { }

    /// <summary>
    /// Creates a new product instance.
    /// </summary>
    /// <param name="id">Unique product ID.</param>
    /// <param name="name">Product name.</param>
    /// <param name="price">Product price.</param>
    /// <param name="createdAt">Optional. Timestamp when the product was created. Defaults to current time if not provided.</param>
    public Product(int id, string name, decimal price, DateTime? createdAt = null)
    {
        Id = id;
        Name = name;
        Price = price;
        CreatedAt = createdAt ?? DateTime.Now;
    }
}
