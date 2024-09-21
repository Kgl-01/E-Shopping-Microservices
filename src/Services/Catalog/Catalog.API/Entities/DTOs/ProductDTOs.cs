namespace Catalog.API.Entities.DTOs
{
    public record CreateProductDTO(string Name, string Category, string Summary, string Description, string ImageFile, decimal Price);

    public record UpdateProductDTO(string Name, string Category, string Summary, string Description, string ImageFile, decimal Price);

    public record ProductDTO(Guid Id, string Name, string Category, string Summary, string Description, string ImageFile, decimal Price);
}
