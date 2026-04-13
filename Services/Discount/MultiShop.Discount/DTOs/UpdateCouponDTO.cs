namespace MultiShop.Discount.DTOs
{
    public class UpdateCouponDTO
    {
        public int CouponId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int Rate { get; set; }
        public DateTime ValidDate { get; set; }
        public bool IsActive { get; set; }
    }
}
