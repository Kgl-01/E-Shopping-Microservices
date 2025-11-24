using Discount.gRPC.Protos;

namespace Basket.API.gRPCServices
{
    public class DiscountgRPCService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
        private readonly ILogger<DiscountgRPCService> _logger;

        public DiscountgRPCService(DiscountProtoService.DiscountProtoServiceClient discountProtoService, ILogger<DiscountgRPCService> logger)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApplyDiscount> GetDiscount(string productName)
        {
            var discount = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discount);
        }
    }
}
