namespace Shopping.Aggregator.Models
{
    public class BasketModel
    {
        public string UserName { get; set; } = string.Empty;

        public List<BasketItemExtendedpModel> Items { get; set; } = new List<BasketItemExtendedModel>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = Items.Aggregate(0m, (prev, curr) => prev + (curr.Price * curr.Quantity));
                return totalPrice;
            }
        }

    }
}
