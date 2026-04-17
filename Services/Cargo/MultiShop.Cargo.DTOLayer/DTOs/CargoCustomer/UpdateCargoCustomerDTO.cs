namespace MultiShop.Cargo.DTOLayer.DTOs.CargoCustomer
{
    public class UpdateCargoCustomerDTO
    {
        public int CargoCustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
