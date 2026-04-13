using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.DTOs;

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
            string query = "INSERT INTO Coupons (Code, Rate, ValidDate, IsActive) VALUES (@Code, @Rate, @ValidDate, @IsActive)";

            using (var connection = _context.CreateConnection())
            {
                // Dapper, DTO içindeki property isimleri @ parametreleriyle aynıysa otomatik eşleştirir.
                await connection.ExecuteAsync(query, createCouponDTO);
            }
        }

        public async Task UpdateCouponAsync(UpdateCouponDTO updateCouponDTO)
        {
            string query = "UPDATE Coupons SET Code=@Code, Rate=@Rate, ValidDate=@ValidDate, IsActive=@IsActive WHERE CouponId=@CouponId";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, updateCouponDTO);
            }
        }

        public async Task DeleteCouponAsync(int id)
        {
            string query = "DELETE FROM Coupons WHERE CouponId=@id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<List<ResultCouponDTO>> GetAllCouponsAsync()
        {
            string query = "SELECT * FROM Coupons";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCouponDTO>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdCouponDTO> GetByIdCouponAsync(int id)
        {
            string query = "SELECT * FROM Coupons WHERE CouponId=@id";

            using (var connection = _context.CreateConnection())
            {
                var value = await connection.QueryFirstOrDefaultAsync<GetByIdCouponDTO>(query, new { id });
                return value!;
            }
        }
    }
}