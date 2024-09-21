using Catalog.API.Entities.Domain;
using Catalog.API.Entities.DTOs;
using Catalog.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsRepository productsRepository, ILogger<ProductsController> logger)
        {
            _productsRepo = productsRepository ?? throw new ArgumentNullException(nameof(_productsRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string category = "")
        {
            var products = await _productsRepo.GetAllAsync();


            if (products == null)
            {
                _logger.LogError($"Products list not found");
                return NotFound();
            }

            var productsDTO = products.Select(p => p.asDTO()).ToList();

            return Ok(productsDTO);
        }



        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            Product? domainModel = await _productsRepo.GetAsync(p => p.Id == id);
            if (domainModel == null)
            {
                return NotFound(id);
            }

            return Ok(domainModel.asDTO());
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO product)
        {
            if (product == null)
            {
                return BadRequest();
            }


            Product domainModel = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                ImageFile = product.ImageFile,
                Price = product.Price,
                Summary = product.Summary,
            };

            var model = await _productsRepo.CreateAsync(domainModel);
            return CreatedAtAction(nameof(GetProductById), new { id = model.Id }, product);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductDTO productDTO)
        {
            var domainModel = await _productsRepo.GetAsync(p => id == p.Id);

            if (domainModel == null)
            {
                return NotFound(id);
            }
            domainModel.Name = productDTO.Name;
            domainModel.Description = productDTO.Description;
            domainModel.Category = productDTO.Category;
            domainModel.Summary = productDTO.Summary;
            domainModel.Price = productDTO.Price;
            domainModel.ImageFile = productDTO.ImageFile;

            await _productsRepo.UpdateAsync(domainModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var product = await _productsRepo.RemoveAsync(id);

            if (product == null)
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
