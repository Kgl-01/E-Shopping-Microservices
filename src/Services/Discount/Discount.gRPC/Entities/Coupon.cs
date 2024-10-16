using System.ComponentModel.DataAnnotations;

namespace Discount.gRPC.Entities
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }

        public static implicit operator bool(Coupon v)
        {
            throw new NotImplementedException();
        }
    }
}
