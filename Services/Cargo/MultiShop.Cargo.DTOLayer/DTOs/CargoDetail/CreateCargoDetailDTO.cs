namespace MultiShop.Cargo.DTOLayer.DTOs.CargoDetail
{
    public class CreateCargoDetailDTO
    {
        public string SenderCustomer { get; set; } = string.Empty;
        public string ReceiverCustomer { get; set; } = string.Empty;
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
    }
}
