using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Common.Data
{
    public interface ICouponContext
    {
        NpgsqlConnection GetConnection();
    }
}
