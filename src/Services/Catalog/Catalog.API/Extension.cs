using Catalog.API.Entities.Domain;
using Catalog.API.Entities.DTOs;

namespace Catalog.API
{
    public static class Extension
    {
        public static ProductDTO asDTO(this Product product)
        {
            return new ProductDTO(Id: product.Id, Name: product.Name, Category: product.Category, Summary: product.Summary, Description: product.Description, ImageFile: product.ImageFile, Price: product.Price);
        }


        public static Product asDomainModel(this CreateProductDTO createProductDTO)
        {

            return new Product
            {
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Category = createProductDTO.Category,
                ImageFile = createProductDTO.ImageFile,
                Price = createProductDTO.Price,
                Summary = createProductDTO.Summary,
            };

        }
    }
}
