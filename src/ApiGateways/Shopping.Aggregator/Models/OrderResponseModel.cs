namespace Shopping.Aggregator.Models
{
    public class OrderResponseModel
    {
        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        //Billing Address
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string? AddressLine { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }


        //Payment
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiration { get; set; }
        public string? CVV { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
