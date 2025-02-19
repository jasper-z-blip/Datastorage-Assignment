namespace WebApi.Models;

public static class ProductType
{
    public static readonly List<ProductModel> Products = new List<ProductModel>
    {
        new ProductModel { Id = 1, Name = "Junior Developer" },
        new ProductModel { Id = 2, Name = "Midlevel Developer" },
        new ProductModel { Id = 3, Name = "Senior Developer" }
    };
}

public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
