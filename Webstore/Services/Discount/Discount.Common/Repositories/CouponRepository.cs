using AutoMapper;
using Dapper;
using Discount.Common.Data;
using Discount.Common.DTOs;
using Discount.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ICouponContext _context;
        private readonly IMapper _mapper;

        public CouponRepository(ICouponContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CouponDTO> GetDiscount(string productName)
        {
            using var connection = _context.GetConnection();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            return _mapper.Map<CouponDTO>(coupon);
        }

        public async Task<bool> CreateDiscount(CreateCouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDiscount(UpdateCouponDTO couponDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CouponDTO>> GetRandomDiscounts(int numberOfDiscounts)
        {
            throw new NotImplementedException();
        }
    }
}
