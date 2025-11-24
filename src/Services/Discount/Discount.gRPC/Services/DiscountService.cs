using Discount.gRPC.Entities;
using Discount.gRPC.Protos;
using Discount.gRPC.Repository;
using Grpc.Core;

namespace Discount.gRPC.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public override async Task<ApplyDiscount> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found"));
            }

            //mapping coupon entity to couponModel entity by custom extension method
            var couponModel = coupon.asCouponModel();

            var applyDiscount = couponModel.asDTO();

            return applyDiscount;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = request.Coupon.toCoupon();
            await _repository.CreateDiscount(coupon);
            _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            _logger.LogInformation("Discount is successfully fetched. ProductName : {ProductName} of {Amount}", coupon.ProductName, coupon.Amount);

            CouponModel couponModel = coupon.asCouponModel();

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            Coupon coupon = request.Coupon.toCoupon();
            Coupon updatedCoupon = await _repository.UpdateDiscount(coupon);

            if (updatedCoupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={coupon.ProductName} is not found"));
            }

            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            return updatedCoupon.asCouponModel();
        }

        public override async Task<DeleteDiscountReponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);

            DeleteDiscountReponse response = new DeleteDiscountReponse { Success = deleted };

            return response;
        }

    }
}
