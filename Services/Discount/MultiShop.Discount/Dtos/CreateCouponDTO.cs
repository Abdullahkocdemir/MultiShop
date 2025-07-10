namespace MultiShop.Discount.Dtos
{
    public class CreateCouponDTO
    {
        public string Code { get; set; } = string.Empty;
        public int Rate { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset ValidDate { get; set; }
    }
}
