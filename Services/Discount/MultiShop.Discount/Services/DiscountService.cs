using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MultiShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateCouponAsync(CreateCouponDTO createCouponDTO)
        {
            string query = "insert into Coupons(Code,Rate,IsActive,ValidDate) values(@code,@rate,@isactive,@validDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@code", createCouponDTO.Code);
            parameters.Add("@rate", createCouponDTO.Rate);
            parameters.Add("@isactive", createCouponDTO.IsActive);
            parameters.Add("@validDate", createCouponDTO.ValidDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task DeleteCouponAsync(int id)
        {
            string query = "Delete From Coupons where CouponId=@couponıd";
            var parameters = new DynamicParameters();
            parameters.Add("@couponıd", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultCouponDTO>> GetAllCouponAsync()
        {
            string query = "Select * From Coupons";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDTO>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdCouponDTO> GetByIdCouponAsync(int id)
        {
            string query = "Select * From Coupons Where CouponId=@couponid";
            var parameters = new DynamicParameters();
            parameters.Add("@couponid", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDTO>(query, parameters);
                return values!;
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDTO updateCouponDTO)
        {
            string query = "Update  Coupons Set Code=@code,Rate=@rate,IsActive=@isactive,ValidDate=@validDate where CouponId=@couponıd";
            var parameters = new DynamicParameters();
            parameters.Add("@code", updateCouponDTO.Code);
            parameters.Add("@rate", updateCouponDTO.Rate);
            parameters.Add("@isactive", updateCouponDTO.IsActive);
            parameters.Add("@validDate", updateCouponDTO.ValidDate);
            parameters.Add("@couponıd", updateCouponDTO.CouponId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
