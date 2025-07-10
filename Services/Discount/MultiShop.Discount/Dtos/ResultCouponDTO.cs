namespace MultiShop.Discount.Dtos
{
    public class ResultCouponDTO
    {
        public int CouponId { get; set; }
        public string Code { get; set; } = string.Empty;
        public int Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset ValidDate { get; set; }
    }
}
