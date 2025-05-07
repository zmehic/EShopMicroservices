using DiscountgRPC.Data;
using DiscountgRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountgRPC.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger)
        :DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
            if(coupon is null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };

            logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amoun: {amomunt}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

            await dbContext.Coupons.AddAsync(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succcessfully created. ProductName : {productName}, Amount : {amount}, Description : {description}", coupon.ProductName, coupon.Amount, coupon.Description);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succcessfully updated. ProductName : {productName}, Amount : {amount}, Description : {description}", coupon.ProductName, coupon.Amount, coupon.Description);
            
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName : {request.ProductName} Not Found"));
            }

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succcessfully deleted. ProductName : {productName}", coupon.ProductName);

            return new DeleteDiscountResponse
            {
                Success = true
        };
        }
    }
}
