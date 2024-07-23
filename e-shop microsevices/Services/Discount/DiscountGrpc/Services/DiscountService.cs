using DiscountGrpc.Data;
using DiscountGrpc.Model;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Services
{
    public class DiscountService(DiscountContext dbcontext, ILogger<DiscountService>logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request argument"));
            }
            dbcontext.Coupons.Add(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created for Product name:{name} , Amount:{amount}", coupon.Name, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.Name == request.ProductName);
            if (coupon == null)
            {
                coupon = new Model.Coupon { Name = "No Coupon", Amount = 0, Description = "No Discount" };
            }
            logger.LogInformation("Discount is retreived for Product name:{name} , Amount:{amount}", coupon.Name, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request argument"));
            }
            dbcontext.Coupons.Update(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated for Product name:{name} , Amount:{amount}", coupon.Name, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.Name == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product name ={request.ProductName} is not found"));
            }
            dbcontext.Coupons.Remove(coupon);
            await dbcontext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully removed for Product name:{name}", coupon.Name);
          
            return new DeleteDiscountResponse { Success = true};
        }
    }
}
