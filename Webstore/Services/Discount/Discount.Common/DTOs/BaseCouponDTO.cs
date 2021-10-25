using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.DTOs
{
    public class BaseCouponDTO
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
