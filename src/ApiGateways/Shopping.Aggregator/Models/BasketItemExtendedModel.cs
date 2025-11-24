namespace Shopping.Aggregator.Models
{
    public class BasketItemExtendedModel
    {
        public int Quantity { get; set; }
        public string Color { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        //Product related additional fields
        public string Category { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
    }
}
