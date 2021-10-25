using Discount.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.Repositories
{
    public interface ICouponRepository
    {
        Task<CouponDTO> GetDiscount(string productName);
        Task<bool> CreateDiscount(CreateCouponDTO couponDTO);
        Task<bool> UpdateDiscount(UpdateCouponDTO couponDTO);
        Task<bool> DeleteDiscount(string productName);
        Task<IEnumerable<CouponDTO>> GetRandomDiscounts(int numberOfDiscounts);
    }
}
